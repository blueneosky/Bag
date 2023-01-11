using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RJCP.IO.Ports;

using static MoreLinq.Extensions.ForEachExtension;

namespace Alphonse.Listener.Connectors;

public class LinuxModemConnector : ModemConnector
{

    private readonly AsyncAccessBox<ModemContext> _context = new(new());

    public LinuxModemConnector(
        ILogger<LinuxModemConnector> logger,
        ILogger<ModemHandler> modemHandlerLogger,
        IOptions<AlphonseSettings> modemSettings
        ) : base(logger, modemHandlerLogger, modemSettings)
    {
    }

    public override Task OpenAsync() => this._context.UseAsync(context =>
    {
        if (context.Connector is not null)
            throw new InvalidOperationException("Already opened");

        this.Logger.LogInformation("Opening the modem on '{ModemPort}' ...", this.ModemPort);
        var connector = new SerialPortStream(this.ModemPort, 9600)
        {
            NewLine = "\r\n",
            Encoding = Encoding.ASCII,
            WriteTimeout = 1000,
            ReadTimeout = 100,
        };

        connector.Open();
        this.Logger.LogInformation("Opening the modem on '{ModemPort}' DONE", this.ModemPort);

        context.Connector = connector;

        return Task.CompletedTask;
    });

    public override async Task ListenAsync(IModemDataDispatcher listener, CancellationToken token)
    {
        // Note : listen must keep running until token expired (or any errors)

        var task = Task.FromException(new InvalidOperationException("Task was not initialized"));

        await this._context.UseAsync(async context =>
        {
            this.Logger.LogInformation("Listening setup ...");
            var source = await SetupListenAsync(listener, context, token);
            task = source?.Task;
            this.Logger.LogInformation("Listening setup DONE");
        }, token).ConfigureAwait(false);

        await task;

        await this._context.UseAsync(context =>
        {
            context.ListenTokenSource?.Dispose();
            context.ListenTokenSource = null;

            return Task.CompletedTask;
        }, token).ConfigureAwait(false);
    }

    private async Task<TaskCompletionSource?> SetupListenAsync(IModemDataDispatcher listener, ModemContext context, CancellationToken token)
    {
        if (context.ListenTokenSource is not null)
            throw new InvalidOperationException("Listener already present - please cancel the previous listening or close this instance");

        if (context.Connector is null)
            throw new InvalidOperationException("Modem not opened or closed");

        this.Logger.LogDebug("  input flushing...");
        context.Connector.DiscardInBuffer();

        await this.ModemHandler.SetupListenModeAsync(token).ConfigureAwait(true);

        // Listening
        var listenTaskCompletionSource = new TaskCompletionSource();

        context.ListenTokenSource = CancellationTokenSource.CreateLinkedTokenSource(token);
        context.ListenTokenSource.Token.Register(() => StopListening(null));

        this.Logger.LogDebug("  modem listeners...");
        context.Connector.DataReceived += OnDataReceived;
        context.Connector.ErrorReceived += OnErrorReceived;

        return listenTaskCompletionSource;

        // =================================================================================

        void OnDataReceived(object? sender, SerialDataReceivedEventArgs e)
        {
            if (!(sender is SerialPortStream connector) || connector != context.Connector)
                return;

            if (e.EventType == SerialData.Eof)
            {
                StopListening(null);
                return;
            }

            try
            {
                string? datagram = null;
                while ((datagram = connector.ReadLine()) is not null)
                {
                    var task = this.DispatchDataAsync(listener, datagram, token);
                    if (task.IsCompletedSuccessfully)
                        continue;   // for short/cold tasks
                    Task.Run(() => task).Wait();
                }

                // serial com was closed - stop the listening
                StopListening(null);
            }
            catch (TimeoutException)
            {
                // no more OR not enougth data
            }
        }

        void OnErrorReceived(object? sender, SerialErrorReceivedEventArgs e)
        {
            if (!(sender is SerialPortStream connector) || connector != context.Connector)
                return;

            try
            {
                // create a contexte
                throw new InvalidDataException($"[{nameof(SerialPortStream)}] Error received: '{e.EventType.ToString()}'");
            }
            catch (Exception ex)
            {
                // pass to the listener
                StopListening(error: ex);
            }
        }

        void StopListening(Exception? error)
        {
            var connector = context.Connector;
            if (connector is not null)
            {
                connector.DataReceived -= OnDataReceived;
                connector.ErrorReceived -= OnErrorReceived;
            }

            if (error is not null)
            {
                listenTaskCompletionSource?.SetException(error);
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

    public override Task PickupHangupAsync(TimeSpan hangupDelay, CancellationToken token) => this._context.UseAsync(async context =>
    {
        if (context.Connector?.IsOpen != true)
            throw new InvalidOperationException("Modem not opened or closed");

        await this.ModemHandler.DoPickupHangupAsync(hangupDelay, token).ConfigureAwait(true);
    }, token);

    public override Task WriteCommandsAsync(IEnumerable<string> commands, CancellationToken token)
    {
        var connector = this._context.ReadNow().Connector;  // re-entrancy deadlock
        if (connector?.IsOpen != true)
            throw new InvalidOperationException("Modem not opened or closed");

        commands.ForEach(c => connector.WriteLine(c));

        return Task.CompletedTask;
    }

    public override void Close()
    {
        // try gently (only 500ms), then get and close everything
        using var shortTokenSource = new CancellationTokenSource(TimeSpan.FromMilliseconds(500));
        if (!this._context.TryRead(out var context, shortTokenSource.Token))
            context = this._context.ReadNow();

        // close listening, if any
        context.ListenTokenSource?.Cancel(false);
        context.ListenTokenSource?.Dispose();
        context.ListenTokenSource = null;

        // close serial com, if any
        context.Connector?.Close();
        context.Connector = null;
    }

    private record ModemContext
    {
        public SerialPortStream? Connector { get; set; }
        public CancellationTokenSource? ListenTokenSource { get; set; }
    }
}
