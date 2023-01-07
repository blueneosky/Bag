using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Alphonse.Listener.Dto;
using DotNet.RestApi.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Alphonse.Listener;

public class HistoryPhoneNumberService : IHistoryPhoneNumberService
{
    private readonly ILogger _logger;
    private readonly RestApiClient _restApiClient;
    private readonly string _webAppBaseUri;

    public HistoryPhoneNumberService(
        ILogger<HistoryPhoneNumberService> logger,
        IOptions<AlphonseSettings> alphonseSettings,
        RestApiClient restApiClient)
    {
        this._logger = logger;
        this._restApiClient = restApiClient;

        this._webAppBaseUri = alphonseSettings.Value.WebAppBaseUri;
    }

    public Task<CallHistoryDto> RegisterIncommingCallAsync(CallHistoryDto callHistory, CancellationToken token)
        => this.CallAsync(
            HttpMethod.Post, new Uri($"{this._webAppBaseUri}/CallHistory"),
            callHistory, true, token
        );

    public Task UpdateHistoryCallAsync(CallHistoryDto callHistory, CancellationToken token)
        => this.CallAsync(
            HttpMethod.Put, new Uri($"{this._webAppBaseUri}/CallHistory/{callHistory.CallHistoryId}"),
            callHistory, false, token
        );

    private async Task<CallHistoryDto> CallAsync(HttpMethod httpMethod, Uri uri, CallHistoryDto callHistory, bool readResponse, CancellationToken token)
    {
        try
        {
            using var response = await this._restApiClient.SendJsonRequest(httpMethod, uri, callHistory);
            if (!response.IsSuccessStatusCode)
            {
                this._logger.LogDebug("Something went wrong when calling to [{Methode}] {Uri}\nWith Payload: {Data}", httpMethod, uri, Newtonsoft.Json.JsonConvert.SerializeObject(callHistory, Newtonsoft.Json.Formatting.Indented));
                var message = await response.Content.ReadAsStringAsync(token);
                this._logger.LogDebug("Response [{StatusCode}]: {Response}", response.StatusCode, message);
            }
            response.EnsureSuccessStatusCode();
            if (readResponse)
                callHistory = await response.DeseriaseJsonResponseAsync<CallHistoryDto>();

            this._logger.LogInformation("History updated for '{HNumber}' from WebApi", callHistory.UCallNumber);

            return callHistory;
        }
        catch (Exception ex)
        {
            Debug.Fail("Check what append");
            this._logger.LogWarning(ex, "Fail to register incomming for '{HNumber}' from WebApi", callHistory.UCallNumber);
            return null;
        }
    }

}