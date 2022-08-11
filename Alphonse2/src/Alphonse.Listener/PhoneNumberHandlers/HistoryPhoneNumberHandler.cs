using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Alphonse.Listener.Dto;
using DotNet.RestApi.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Alphonse.Listener.PhoneNumberHandlers;

public class HistoryPhoneNumberHandler : IPhoneNumberHandler
{
    private readonly ILogger _logger;
    private readonly RestApiClient _restApiClient;
    private readonly string _webAppBaseUri;

    public HistoryPhoneNumberHandler(
        ILogger<HistoryPhoneNumberHandler> logger,
        IOptions<AlphonseSettings> alphonseSettings,
        RestApiClient restApiClient)
    {
        this._logger = logger;
        this._restApiClient = restApiClient;

        this._webAppBaseUri = alphonseSettings.Value.WebAppBaseUri;
    }

    public Task ProcessAsync(IPhoneNumberHandlerContext context, CancellationToken token)
    {
        Task.Run(SendHistoryAsync);
        return Task.CompletedTask;

        //=====================================

        async Task SendHistoryAsync()
        {
            try
            {
                var uri = new Uri($"{this._webAppBaseUri}/CallHistory/");
                var callHistory = new CallHistoryDto
                {
                    Timestamp = context.Timestamp.UtcDateTime,
                    UCallNumber = context.Number,
                };
                var response = await this._restApiClient.SendJsonRequest(HttpMethod.Post, uri, callHistory);
                response.EnsureSuccessStatusCode();
                callHistory = await response.DeseriaseJsonResponseAsync<CallHistoryDto>();
                this._logger.LogInformation("History updated for '{HNumber}' [{Number}] from WebApi", context.Number.ToString(true), context.Number);
            }
            catch (Exception ex)
            {
                Debug.Fail("Check what append");
                this._logger.LogWarning(ex, "Fail to Oost history for '{HNumber}' [{Number}] from WebApi", context.Number.ToString(true), context.Number);
            }
        }
    }
}