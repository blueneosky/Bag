using System.IO.Ports;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Alphonse.Listener.Connectors;

public abstract class ModemConnector : IModemConnector
{
    protected ILogger Logger { get; }
    protected ModemHandler ModemHandler { get; }
    protected string ModemPort { get; }

    protected ModemConnector(
        ILogger<ModemConnector> logger,
        ILogger<ModemHandler> modemHandlerLogger,
        IOptions<AlphonseSettings> modemSettings)
    {
        this.Logger = logger;
        this.ModemHandler = new ModemHandler(modemHandlerLogger, this);
        this.ModemPort = GetModemPort();

        //===================================

        string GetModemPort()
        {
            var modemPort = modemSettings.Value.ModemPort;
            if (!string.IsNullOrWhiteSpace(modemPort))
                return modemPort;

            logger.LogWarning("[init] ModemPort was not defined in configuration");
            logger.LogInformation("[init] Auto-detection of serial interfaces...");
            var portNames = SerialPort.GetPortNames();  // note : not bad for this task
            if (!portNames.Any())
                throw new InvalidOperationException("No serial interface found");

            var messageBuilder = portNames.Aggregate(
                new StringBuilder("Serial interfaces found:"),
                (sb, pn) => sb.AppendLine().Append(pn));
            logger.LogDebug(messageBuilder.ToString());

            modemPort = portNames.First();
            logger.LogInformation("[init] Selected serial interface is '{ModemPort}'", modemPort);

            return modemPort;
        }
    }

    public abstract Task OpenAsync();

    public abstract Task ListenAsync(IModemDataDispatcher listener, CancellationToken token);

    protected virtual Task DispatchDataAsync(IModemDataDispatcher listener, string datagram, CancellationToken token)
    {
        this.Logger.LogTrace("DatagramReceived: {Datagram}", datagram);

        var dataType = this.ModemHandler.TryGetData(datagram, out var data);
        if(dataType == ModemDataType.None)
            return Task.CompletedTask;  // empty line

        this.Logger.LogDebug("Dispatch data: [{DataType}]:{Data}", dataType, data);
        return listener.DispatchAsync(dataType, data, token);
    }

    public abstract Task PickupHangupAsync(TimeSpan hangupDelay, CancellationToken token);

    public abstract Task WriteCommandsAsync(IEnumerable<string> commands, CancellationToken token);

    public abstract void Close();

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        this.Close();
    }
}
