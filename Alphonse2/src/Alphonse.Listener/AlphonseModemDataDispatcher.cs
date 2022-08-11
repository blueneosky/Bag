using System.Diagnostics;
using Alphonse.Listener.Dto;
using Alphonse.Listener.PhoneNumberHandlers;
using DotNet.RestApi.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Alphonse.Listener;

public class AlphonseModemDataDispatcher : IModemDataDispatcher
{
    private readonly ILogger<AlphonseModemDataDispatcher> _logger;
    private readonly PhonebookService _phonebookService;
    private readonly IEnumerable<IPhoneNumberHandler> _phoneNumberHandlers;

    public AlphonseModemDataDispatcher(
        ILogger<AlphonseModemDataDispatcher> logger,
        PhonebookService phonebookService,
        IEnumerable<IPhoneNumberHandler> phoneNumberHandlers)
    {
        this._logger = logger;
        this._phonebookService = phonebookService;
        this._phoneNumberHandlers = phoneNumberHandlers;
    }

    public async Task DispatchAsync(string data, CancellationToken token)
    {
        var timestamp = DateTimeOffset.UtcNow;

        if (data == Modem.CONST_MODEM_OK)
        {
            this._logger.LogTrace("[ignored] {Data}", data);
            return;
        }

        if (data == Modem.CONST_MODEM_RING)
        {
            this._logger.LogInformation("RING tone received", data);
            return;
        }

        var index = data.IndexOf(Modem.CONST_MODEM_NUMBER_TAG);
        if (index < 0)
        {
            this._logger.LogTrace("[ignored] {Data}", data);
            return;
        }

        var rawNumber = data.Substring(Modem.CONST_MODEM_NUMBER_TAG.Length);
        var context = await BuildContextAsync(rawNumber).ConfigureAwait(false);
        if (context is null)
        {
            this._logger.LogInformation("[ignored] Missing or invalid phone number");
            return;
        }

        context.Timestamp = timestamp;

        this._logger.LogInformation("Incoming call from '{HNumber}' [{Number}]", context.PhoneNumber?.Name ?? context.Number.ToString(true), context.Number);

        await this.ProcessAsync(context, token).ConfigureAwait(false);
    }

    private async Task<PhoneNumberHandlerContext?> BuildContextAsync(string rawNumber)
    {
        if (!PhoneNumber.TryParse(rawNumber, out var number))
            return null;

        var (found, phoneNumber) = await this._phonebookService.TryGetPhoneNumberAsync(number)
            .ConfigureAwait(false);
        if (!found)
        {
            this._logger.LogDebug("Unknown number '{HNumber}' [{Number}]", number.ToString(true), number);
            return new() { Number = number, PhoneNumber = null };
        }

        return new() { Number = number, PhoneNumber = phoneNumber };
    }

    private async Task ProcessAsync(PhoneNumberHandlerContext context, CancellationToken token)
    {
        foreach (var handler in this._phoneNumberHandlers)
        {
            await ProcessAsync(handler).ConfigureAwait(false);

            if (token.IsCancellationRequested)
            {
                this._logger.LogInformation("Fast abort due to cancellation requested");
                break;
            }

            if (context.StopProcessing)
                break;
        }

        //=======================================================

        async Task ProcessAsync(IPhoneNumberHandler handler)
        {
            try
            {
                await handler.ProcessAsync(context, token).ConfigureAwait(false);
            }
            catch (TaskCanceledException) { }
            catch (Exception ex)
            {
                this._logger.LogWarning(ex, "Unexpected error occured during dispatching on {HandlerName}", handler.GetType().Name);
            }
        }
    }

    private sealed class PhoneNumberHandlerContext : IPhoneNumberHandlerContext
    {
        public DateTimeOffset Timestamp { get; set; }

        public PhoneNumber Number { get; set; } = null!;

        public PhoneNumberDto? PhoneNumber { get; set; }

        public bool StopProcessing { get; set; }
    }
}