using System.Diagnostics;
using Alphonse.Listener.Dto;
using DotNet.RestApi.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Alphonse.Listener;

public class HistoryPhoneNumberService : IHistoryPhoneNumberService
{
    private readonly ILogger _logger;
    private readonly ServiceFactory<RestApiClient> _restApiClient;
    private readonly string _webApiBaseUri;

    public HistoryPhoneNumberService(
        ILogger<HistoryPhoneNumberService> logger,
        IOptions<AlphonseSettings> alphonseSettings,
        ServiceFactory<RestApiClient> restApiClient)
    {
        this._logger = logger;
        this._restApiClient = restApiClient;

        this._webApiBaseUri = alphonseSettings.Value.WebApiBaseUri;
    }

    public Task<CallHistoryDto> RegisterIncommingCallAsync(CallHistoryDto callHistory, CancellationToken token)
        => this.WebCallAsync(
            HttpMethod.Post, new Uri($"{this._webApiBaseUri}/CallHistory"),
            callHistory, true, token
        );

    public Task UpdateHistoryCallAsync(CallHistoryDto callHistory, CancellationToken token)
        => this.WebCallAsync(
            HttpMethod.Put, new Uri($"{this._webApiBaseUri}/CallHistory/{callHistory.CallHistoryId}"),
            callHistory, false, token
        );

    private async Task<CallHistoryDto> WebCallAsync(HttpMethod httpMethod, Uri uri, CallHistoryDto callHistory, bool readResponse, CancellationToken token)
    {
        try
        {
            RestApiClient client = this._restApiClient;
            
            using var response = await client.SendJsonRequest(httpMethod, uri, callHistory);
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