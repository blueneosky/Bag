using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Alphonse.Listener;

public class Modem : IDisposable
{
    private static readonly string[] CONST_MODEM_RESET = new[] { "ATZ" };  // reset
    private static readonly string[] CONST_MODEM_INIT = new[] { "ATE0", "AT+VCID=1" };  // no echo, activate call id printing
    private static readonly string[] CONST_MODEM_PICKUP = new[] { "ATA" };  // will provide a nice variations of modem strident sound
    private static readonly string[] CONST_MODEM_HANGUP = new[] { "ATH" };
    public const string CONST_MODEM_OK = "OK";
    public const string CONST_MODEM_RING = "RING";
    public const string CONST_MODEM_NUMBER_TAG = "NMBR = ";

    private readonly ILogger _logger;
    private readonly string _modemPort;

    private SerialPort? _serialPort;

    private readonly AsyncLock _gate = new();
    private CancellationToken _listenToken = CancellationToken.None;
    private IModemDataDispatcher? _modemListener;
    private TaskCompletionSource? _listenTaskSource;

    public Modem(ILogger<Modem> logger, IOptions<AlphonseSettings> modemSettings)
    {
        this._logger = logger;
        this._modemPort = GetModemPort();

        //===================================

        string GetModemPort()
        {
            var modemPort = modemSettings.Value.ModemPort;
            if (!string.IsNullOrWhiteSpace(modemPort))
                return modemPort;

            logger.LogWarning("ModemPort was not defined in configuration");
            logger.LogInformation("Auto-detection of serial interfaces...");
            var portNames = SerialPort.GetPortNames();
            if (!portNames.Any())
                throw new InvalidOperationException("No serial interface found");

            var messageBuilder = portNames.Aggregate(
                new StringBuilder("Serial interfaces found:"),
                (sb, pn) => sb.AppendLine().Append(pn));
            logger.LogDebug(messageBuilder.ToString());

            modemPort = portNames.First();
            logger.LogInformation("Selected serial interface is '{ModemPort}'", modemPort);

            return modemPort;
        }
    }

    public void Open()
    {
        this.Close();

        _serialPort = new SerialPort(this._modemPort, 9600)
        {
            NewLine = "\r\n",
            Encoding = Encoding.ASCII,
            WriteTimeout = 1000,
            ReadTimeout = 1000,
        };

        this._logger.LogInformation("Opening the modem on '{ModemPort}' ...", this._modemPort);
        this._serialPort.Open();

        this._logger.LogDebug("  input flushing...");
        this._serialPort.DiscardInBuffer();

        this._logger.LogDebug("  send reset...");
        this.WriteCommands(CONST_MODEM_RESET);

        this._logger.LogDebug("  modem init...");
        this.WriteCommands(CONST_MODEM_INIT);

        this._serialPort.DataReceived += DataReceived;
        this._serialPort.ErrorReceived += ErrorReceived;
        this._logger.LogInformation("Opening the modem on '{ModemPort}' DONE", this._modemPort);
    }

    private void DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        var serialPort = (SerialPort)sender;
        Debug.Assert(serialPort == this._serialPort);

        this._gate.Run(Lock);

        //====================================================
        void Lock()
        {
            try
            {
                string? data = null;
                while ((data = serialPort.ReadLine()) is not null)
                {
                    data = data.Trim('\r', '\n');   // sometime here
                    if(string.IsNullOrWhiteSpace(data))
                        continue;   // no need to process empty lines

                    this._logger.LogTrace("DataReceived: {Data}", data);
                    if (_modemListener is null)
                        continue;

                    this._logger.LogDebug("Dispatch data: {Data}", data);
                    var task = Task.Run(() => this._modemListener.DispatchAsync(data, this._listenToken));
                    task.Wait();
                }

                // serial com was closed - stop the listening
                this.StopListening();
            }
            catch (TimeoutException)
            {
                // no more OR not enougth data
            }
        }
    }

    private void ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
    {
        try
        {
            // create a contexte
            throw new InvalidDataException($"[SerialPort] Error received: '{e.EventType.ToString()}'");
        }
        catch (Exception ex)
        {
            // pass to the listener
            StopListening(error: ex);
        }
    }

    public Task ListenAsync(IModemDataDispatcher listener, CancellationToken token)
    {
        if (this._serialPort is null)
            throw new ObjectDisposedException("Serial port is not open");

        var completionSource = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

        this._gate.Run(Lock);

        //====================================================
        void Lock()
        {
            token.Register(() => this.StopListening(token: token));
            this._listenToken = token;
            this._modemListener = listener;
            this._listenTaskSource = completionSource;
        }

        return completionSource.Task;
    }

    public void Close()
    {
        if (this._serialPort is null)
            return;

        this.StopListening();   // no error/cancellation

        this._serialPort.ErrorReceived -= ErrorReceived;
        this._serialPort.DataReceived -= DataReceived;

        this._serialPort.Close();
        this._serialPort = null;
    }

    private void StopListening(Exception? error = null, CancellationToken? token = null)
    {
        this._gate.Run(Lock);

        //===============================

        void Lock()
        {
            var modemListener = Interlocked.CompareExchange(ref this._modemListener, null, null);
            if (modemListener is null)
                return;

            var listenTaskSource = Interlocked.CompareExchange(ref this._listenTaskSource, null, null);
            Debug.Assert(listenTaskSource is not null);
            // end of ListenAsync method
            if (error is not null)
            {
                listenTaskSource.SetException(error);
            }
            else if (token is not null)
            {
                listenTaskSource.SetCanceled(token.Value);
            }
            else
            {
                listenTaskSource.SetResult();
            }

            this._listenToken = CancellationToken.None;
            this._modemListener = null;
            this._listenTaskSource = null;
        }
    }

    public void WriteCommands(params string[] commands)
        => this.WriteCommands((IEnumerable<string>)commands);

    public void WriteCommands(IEnumerable<string> commands)
    {
        if (this._serialPort is null)
            throw new InvalidOperationException("Modem not opened or closed");

        foreach (var command in commands)
        {
            this._serialPort.WriteLine(command);
        }
    }

      public async Task PickupHangupAsync(TimeSpan hangupDelay, CancellationToken token)
        {
            this._logger.LogDebug("Pick up...");
            this.WriteCommands(Modem.CONST_MODEM_PICKUP);
            this._logger.LogDebug("Pick up DONE");
            try
            {
                await Task.Delay(hangupDelay, token);
            }
            finally
            {
                this._logger.LogDebug("Hang up...");
                this.WriteCommands(Modem.CONST_MODEM_HANGUP);
                this._logger.LogDebug("Hang up DONE");
            }
        }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        this.Close();
    }
}
