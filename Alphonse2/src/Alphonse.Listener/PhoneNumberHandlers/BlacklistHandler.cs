using Alphonse.Listener.Connectors;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Alphonse.Listener.PhoneNumberHandlers;

public class BlacklistHandler : IPhoneNumberHandler
{
    private readonly ILogger _logger;
    private readonly IModemConnector _connector;
    private readonly TimeSpan _hangupDelay;

    public BlacklistHandler(ILogger<BlacklistHandler> logger, IModemConnector connector, IOptions<AlphonseSettings> settings)
    {
        this._logger = logger;
        this._connector = connector;
        this._hangupDelay = settings.Value.BlacklistHangupDelay ?? TimeSpan.FromSeconds(1);
    }

    public Task ProcessAsync(IPhoneNumberHandlerContext context, CancellationToken token)
    {
        if (context.PhoneNumber?.Allowed != false)
        {
            this._logger.LogTrace("No match for blacklist - skipped");
            return Task.CompletedTask;
        }

        this._logger.LogInformation("Matched in blacklist - Hangup/Pickup for {Seconds}sec", this._hangupDelay.TotalSeconds);
        context.ActionProcessed = "Blocked";
        context.StopProcessing = true;

        return this._connector.PickupHangupAsync(this._hangupDelay, token);
    }

  
}