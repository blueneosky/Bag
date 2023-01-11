using Microsoft.Extensions.Logging;

namespace Alphonse.Listener.Connectors;

public class ModemHandler
{
    private static readonly string[] CONST_MODEM_RESET = new[] { "ATZ" };  // reset
    private static readonly string[] CONST_MODEM_INIT = new[] { "ATE0", "ATH0", "AT+VCID=1" };  // no echo, ensure hook status, activate call id printing
    private static readonly string[] CONST_MODEM_PICKUP = new[] { "ATA" };  // will provide a nice variations of modem strident sound
    private static readonly string[] CONST_MODEM_HANGUP = new[] { "" };
    private static readonly string[] CONST_MODEM_ASK_CALL_STATE = new[] { "ATX0" }; // will provide th actual state of the calling OK, CONNECT, RING, NO CARRIER, NO ANSWER et ERROR
    private const string CONST_MODEM_OK = "OK";
    private const string CONST_MODEM_ATE0 = "ATE0";
    private const string CONST_MODEM_RING = "RING";
    private const string CONST_MODEM_NO_CARRIER = "NO CARRIER";
    private const string CONST_MODEM_DATE_TAG = "DATE = ";
    private const string CONST_MODEM_TIME_TAG = "TIME = ";
    private const string CONST_MODEM_NUMBER_TAG = "NMBR = ";
    private const string CONST_MODEM_NAME_TAG = "NAME = ";

    private readonly ILogger<ModemHandler> _logger;
    private readonly IModemConnector _modemConnector;

    public ModemHandler(ILogger<ModemHandler> logger, IModemConnector modemConnector)
    {
        this._logger = logger;
        this._modemConnector = modemConnector;
    }

    public ModemDataType TryGetData(string datagram, out string? data)
    {
        data = null;

        datagram = datagram.Trim(' ', '\n', '\r');

        var dataType = datagram switch
        {
            "" => ModemDataType.None,
            CONST_MODEM_OK => ModemDataType.Ok,
            CONST_MODEM_RING => ModemDataType.Ring,
            CONST_MODEM_ATE0 => ModemDataType.Ate0,
            CONST_MODEM_NO_CARRIER => ModemDataType.NoCarrier,
            var dg when TryGetTagValue(dg, CONST_MODEM_NUMBER_TAG, out data) => ModemDataType.PhoneNumber,
            var dg when TryGetTagValue(dg, CONST_MODEM_DATE_TAG, out data) => ModemDataType.Date,
            var dg when TryGetTagValue(dg, CONST_MODEM_TIME_TAG, out data) => ModemDataType.Time,
            var dg when TryGetTagValue(dg, CONST_MODEM_NAME_TAG, out data) => ModemDataType.Name,
            _ => ModemDataType.Unmanaged,
        };

        if (!string.IsNullOrWhiteSpace(datagram) && dataType == ModemDataType.Unmanaged)
            this._logger.LogDebug("Unmanaged datagram: {Datagram}", datagram);

        return dataType;

        //===============================================

        bool TryGetTagValue(string datagram, string tag, out string? data)
        {
            if (datagram.StartsWith(tag))
            {
                data = datagram.Substring(tag.Length);
                return true;
            }

            data = null;
            return false;
        }
    }

    private Task WriteCommands(IEnumerable<string> commands, CancellationToken token)
            => this._modemConnector.WriteCommandsAsync(commands, token);

    public async Task SetupListenModeAsync(CancellationToken token = default)
    {
        // Modem initialization for listening
        this._logger.LogDebug("  Command(s) reset...");
        await this.WriteCommands(CONST_MODEM_RESET, token).ConfigureAwait(false);

        this._logger.LogDebug("  Command(s) init...");
        await this.WriteCommands(CONST_MODEM_INIT, token).ConfigureAwait(false);
    }

    public async Task DoPickupHangupAsync(TimeSpan hangupDelay, CancellationToken token = default)
    {
        this._logger.LogDebug("  Command(s) pick up...");
        await this.WriteCommands(CONST_MODEM_PICKUP, token).ConfigureAwait(false);
        this._logger.LogDebug("  Command(s) pick up DONE");
        try
        {
            await Task.Delay(hangupDelay, token);
        }
        finally
        {
            this._logger.LogDebug("  Command(s) hang up...");
            await this.WriteCommands(CONST_MODEM_HANGUP, token).ConfigureAwait(false);
            this._logger.LogDebug("  Command(s) hang up DONE");
        }
    }
}
