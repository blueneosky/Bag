using System.IO.Ports;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Alphonse.Listener;

public class Modem : IModem
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

    private readonly AsyncAccessBox<ModemContext> _context = new(new());

    public Modem(
        ILogger<Modem> logger,
        IOptions<AlphonseSettings> modemSettings)
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

    public void Open() => this._context.Use(context =>
    {
        if (context.SerialPort is not null)
            return;

        this._logger.LogInformation("Opening the modem on '{ModemPort}' ...", this._modemPort);
        var serialPort = new SerialPort(this._modemPort, 9600)
        {
            NewLine = "\r\n",
            Encoding = Encoding.ASCII,
            WriteTimeout = 1000,
            ReadTimeout = 100,
        };

        serialPort.Open();
        this._logger.LogInformation("Opening the modem on '{ModemPort}' DONE", this._modemPort);

        context.SerialPort = serialPort;
    });

    public Task ListenAsync(IModemDataDispatcher listener, CancellationToken token)
    {
        // Note : listen must keep running until token expired (or any errors)

        var task = Task.FromException(new InvalidOperationException("Task was not initialized"));

        this._context.Use(context =>
        {
            this._logger.LogInformation("Listening setup ...");
            var source = SetupListen(listener, context, token);
            task = source?.Task;
            this._logger.LogInformation("Listening setup DONE");
        }, token);

        return task;
    }

    private TaskCompletionSource? SetupListen(IModemDataDispatcher listener, ModemContext context, CancellationToken token)
    {
        if (context.ListenTokenSource is not null)
            throw new InvalidOperationException("Listener already present - please cancel the previous listening or close this instance");

        if (context.SerialPort is null)
            throw new InvalidOperationException("Modem not opened or closed");

        // Modem initialization for listening
        this._logger.LogDebug("  input flushing...");
        context.SerialPort.DiscardInBuffer();

        this._logger.LogDebug("  send reset...");
        context.SerialPort.WriteLines(CONST_MODEM_RESET);

        this._logger.LogDebug("  modem init...");
        context.SerialPort.WriteLines(CONST_MODEM_INIT);

        // Listening
        var listenTaskCompletionSource = new TaskCompletionSource();

        context.ListenTokenSource = CancellationTokenSource.CreateLinkedTokenSource(token);
        context.ListenTokenSource.Token.Register(() => StopListening(null));

        this._logger.LogDebug("  modem listeners...");
        context.SerialPort.DataReceived += OnDataReceived;
        context.SerialPort.ErrorReceived += OnErrorReceived;

        return listenTaskCompletionSource;

        // =================================================================================

        void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var serialPort = (SerialPort)sender;
            if (serialPort != context.SerialPort)
                return;

            if (e.EventType == SerialData.Eof)
            {
                StopListening(null);
                return;
            }

            try
            {
                string? data = null;
                while ((data = serialPort.ReadLine()) is not null)
                {
                    data = data.Trim('\r', '\n');   // sometime here
                    if (string.IsNullOrWhiteSpace(data))
                        continue;   // no need to process empty lines

                    this._logger.LogTrace("DataReceived: {Data}", data);
                    if (listener is null)
                        continue;

                    this._logger.LogDebug("Dispatch data: {Data}", data);
                    var task = Task.Run(() => listener.DispatchAsync(data, token));
                    task.Wait();
                }

                // serial com was closed - stop the listening
                StopListening(null);
            }
            catch (TimeoutException)
            {
                // no more OR not enougth data
            }
        }

        void OnErrorReceived(object sender, SerialErrorReceivedEventArgs e)
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

        void StopListening(Exception? error)
        {
            var serialPort = context.SerialPort;
            if (serialPort is not null)
            {
                serialPort.DataReceived -= OnDataReceived;
                serialPort.ErrorReceived -= OnErrorReceived;
            }

            if (error is not null)
            {
                listenTaskCompletionSource.SetException(error);
            }
            else if (token.IsCancellationRequested)
            {
                // legit cancellation
                listenTaskCompletionSource?.SetCanceled(token);
            }
            else
            {
                // should come from a Close()
                listenTaskCompletionSource?.SetResult();
            }
        }
    }

    public Task PickupHangupAsync(TimeSpan hangupDelay, CancellationToken token) => this._context.UseAsync(async context =>
    {
        if (context.SerialPort is null)
            throw new InvalidOperationException("Modem not opened or closed");

        this._logger.LogDebug("Pick up...");
        context.SerialPort.WriteLines(Modem.CONST_MODEM_PICKUP);
        this._logger.LogDebug("Pick up DONE");
        try
        {
            await Task.Delay(hangupDelay, token);
        }
        finally
        {
            this._logger.LogDebug("Hang up...");
            context.SerialPort.WriteLines(Modem.CONST_MODEM_HANGUP);
            this._logger.LogDebug("Hang up DONE");
        }
    }, token);

    public void Close()
    {
        // try gently (only 500ms), then get and close everything
        if (!this._context.TryRead(out var context, new CancellationTokenSource(TimeSpan.FromMilliseconds(500)).Token))
            context = this._context.ReadNow();

        // close listening, if any
        context.ListenTokenSource?.Cancel(false);
        context.ListenTokenSource = null;

        // close serial com, if any
        context.SerialPort?.Close();
        context.SerialPort = null;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        this.Close();
    }

    private record ModemContext
    {
        public SerialPort? SerialPort { get; set; }
        public CancellationTokenSource? ListenTokenSource { get; set; }
    }
}
