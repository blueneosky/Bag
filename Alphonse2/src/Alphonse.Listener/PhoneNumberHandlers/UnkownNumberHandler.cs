using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Alphonse.Listener.PhoneNumberHandlers;

public class UnkownNumberHandler : IPhoneNumberHandler
{
    private readonly ILogger _logger;
    private readonly Modem _modem;
    private readonly TimeSpan _hangupDelay;

    public UnkownNumberHandler(ILogger<UnkownNumberHandler> logger, Modem modem, IOptions<AlphonseSettings> settings)
    {
        this._logger = logger;
        this._modem = modem;
        this._hangupDelay = settings.Value.UnknownNumberHangupDelay ?? TimeSpan.FromSeconds(13);
    }

    public Task ProcessAsync(IPhoneNumberHandlerContext context, CancellationToken token)
    {
        this._logger.LogInformation("Unknown number or without rule - Hangup/Pickup for {Seconds}sec", this._hangupDelay.TotalSeconds);

        return this._modem.PickupHangupAsync(this._hangupDelay, token);
    }
}