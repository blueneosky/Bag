using Microsoft.Extensions.Logging;

namespace Alphonse.Listener.PhoneNumberHandlers;

public class WhitelistHandler : IPhoneNumberHandler
{
    private readonly ILogger _logger;

    public WhitelistHandler(ILogger<WhitelistHandler> logger)
    {
        this._logger = logger;
    }

    public Task ProcessAsync(IPhoneNumberHandlerContext context, CancellationToken token)
    {
        if (context.PhoneNumber?.Allowed != true)
        {
            this._logger.LogTrace("No match for whitelist - skipped");
            return Task.CompletedTask;
        }

        this._logger.LogInformation("Matched in whitelist - no modem action");
        context.StopProcessing = true;
        context.ActionProcessed = "Accepted";
        return Task.CompletedTask;
    }
}