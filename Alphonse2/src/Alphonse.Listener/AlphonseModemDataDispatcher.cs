using System.Diagnostics;
using Alphonse.Listener.Connectors;
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
    private readonly IHistoryPhoneNumberService _historyPhoneNumberService;
    private readonly IEnumerable<IPhoneNumberHandler> _phoneNumberHandlers;

    public AlphonseModemDataDispatcher(
        ILogger<AlphonseModemDataDispatcher> logger,
        PhonebookService phonebookService,
        IHistoryPhoneNumberService historyPhoneNumberService,
        IEnumerable<IPhoneNumberHandler> phoneNumberHandlers)
    {
        this._logger = logger;
        this._phonebookService = phonebookService;
        this._historyPhoneNumberService = historyPhoneNumberService;
        this._phoneNumberHandlers = phoneNumberHandlers;
    }

    public Task DispatchAsync(ModemDataType dataType, string? data, CancellationToken token)
    {
        var timestamp = DateTimeOffset.UtcNow;

        switch (dataType)
        {
            case ModemDataType.Ok:
                this._logger.LogTrace("[ignored] {Data}", data);
                return Task.CompletedTask;

            case ModemDataType.Ring:
                this._logger.LogInformation("RING tone received", data);
                return Task.CompletedTask;

            case ModemDataType.PhoneNumber:
                return HandlePhoneNumberAsync(data);

            case ModemDataType.Unmanaged:
            default:
                this._logger.LogTrace("[ignored] {Data}", data);
                return Task.CompletedTask;
        }

        //==========================================================
        
        async Task HandlePhoneNumberAsync(string? rawNumber)
        {
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
    }

    private async Task<PhoneNumberHandlerContext?> BuildContextAsync(string? rawNumber)
    {
        if(rawNumber is null)
            return null;

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
        // start registering in parallel in order to reduce latency
        var registering = Task.Run(() =>
        {
            var callHistory = new CallHistoryDto
            {
                Timestamp = context.Timestamp.UtcDateTime,
                UCallNumber = context.Number,
                Action = "Incomming",
            };

            return this._historyPhoneNumberService.RegisterIncommingCallAsync(callHistory, token);
        });

        // apply handlers
        foreach (var handler in this._phoneNumberHandlers)
        {
            if (token.IsCancellationRequested)
            {
                this._logger.LogInformation("Fast abort due to cancellation requested");
                break;
            }

            await ProcessAsync(handler).ConfigureAwait(false);
            if (context.StopProcessing)
                break;
        }

        // re-sync with parallel registration and update with 'action'
        var callHistory = await registering.ConfigureAwait(false);
        if (callHistory is not null)
        {
            callHistory.Action = context.ActionProcessed;
            await this._historyPhoneNumberService.UpdateHistoryCallAsync(callHistory, token);
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
        public string? ActionProcessed { get; set; }
    }
}