using Alphonse.Listener.Connectors;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Alphonse.Listener.PhoneNumberHandlers;

public class UnkownNumberHandler : IPhoneNumberHandler
{
    private readonly ILogger _logger;
    private readonly IModemConnector _connector;
    private readonly TimeSpan _hangupDelay;

    public UnkownNumberHandler(ILogger<UnkownNumberHandler> logger, IModemConnector connector, IOptions<AlphonseSettings> settings)
    {
        this._logger = logger;
        this._connector = connector;
        this._hangupDelay = settings.Value.UnknownNumberHangupDelay ?? TimeSpan.FromSeconds(13);
    }

    public Task ProcessAsync(IPhoneNumberHandlerContext context, CancellationToken token)
    {
        this._logger.LogInformation("Unknown number or without rule - Hangup/Pickup for {Seconds}sec", this._hangupDelay.TotalSeconds);
        context.ActionProcessed = "Muted";
        context.StopProcessing = true;

        return this._connector.PickupHangupAsync(this._hangupDelay, token);
    }
}