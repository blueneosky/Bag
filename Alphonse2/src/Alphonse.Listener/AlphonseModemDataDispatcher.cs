using Microsoft.Extensions.Logging;

namespace Alphonse.Listener;

public class AlphonseModemDataDispatcher : IModemDataDispatcher
{
    private readonly ILogger<AlphonseModemDataDispatcher> _logger;
    private readonly IEnumerable<IPhoneNumberHandler> _phoneNumberHandlers;

    public AlphonseModemDataDispatcher(ILogger<AlphonseModemDataDispatcher> logger, IEnumerable<IPhoneNumberHandler> phoneNumberHandlers)
    {
        this._logger = logger;
        this._phoneNumberHandlers = phoneNumberHandlers;
    }

    public Task DispatchAsync(string data, CancellationToken token)
    {
        if (data == Modem.CONST_MODEM_OK)
        {
            this._logger.LogTrace("[ignored] {Data}", data);
            return Task.CompletedTask;
        }

        if (data == Modem.CONST_MODEM_RING)
        {
            this._logger.LogInformation("RING tone received", data);
            return Task.CompletedTask;
        }

        var index = data.IndexOf(Modem.CONST_MODEM_NUMBER_TAG);
        if (index < 0)
        {
            this._logger.LogTrace("[ignored] {Data}", data);
            return Task.CompletedTask;
        }

        var rawNumber = data.Substring(Modem.CONST_MODEM_NUMBER_TAG.Length);
        if(!PhoneNumber.TryParse(rawNumber, out var number))
        {
            this._logger.LogInformation("[ignored] Missing or invalid phone number");
            return Task.CompletedTask;
        }

        this._logger.LogInformation("Incoming call from '{HNumber}' [{Number}]", number.ToString(true), number);
        
        return this.ProcessAsync(number, token);
    }

    private async Task ProcessAsync(PhoneNumber number, CancellationToken token)
    {
        foreach (var handlers in this._phoneNumberHandlers)
        {
            var processed = await handlers.ProcessAsync(number, token).ConfigureAwait(false);
            if(processed)
                break;
        }
    }
}