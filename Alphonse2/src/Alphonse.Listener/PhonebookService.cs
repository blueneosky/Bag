using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Alphonse.Listener.Dto;
using DotNet.RestApi.Client;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace Alphonse.Listener;

public class PhonebookService
{
    private readonly ILogger _logger;
    private readonly ServiceFactory<RestApiClient> _restApiClient;
    private readonly string _webApiBaseUri;
    private readonly TimeSpan _updateInterval;
    private readonly CancellationToken _applicationStopping;

    private readonly AsyncLock _cacheGate = new AsyncLock();
    private Dictionary<PhoneNumber, PhoneNumberDto> _cache = new();

    private bool _phonebookUpToDate = false;    // use to reduce log

    public PhonebookService(
        ILogger<PhonebookService> logger,
        IOptions<AlphonseSettings> alphonseSettings,
        ServiceFactory<RestApiClient> restApiClient,
        IHostApplicationLifetime appLifetime)
    {
        this._logger = logger;
        this._restApiClient = restApiClient;

        this._webApiBaseUri = alphonseSettings.Value.WebApiBaseUri;
        this._updateInterval = alphonseSettings.Value.PhonebookUpdateInterval ?? TimeSpan.FromMinutes(10);
        this._applicationStopping = appLifetime.ApplicationStopping;

        Task.Run(PeriodicUpdateAsync);   // background task
    }

    public Task<(bool, PhoneNumberDto?)> TryGetPhoneNumberAsync(PhoneNumber number)
    {
        return this._cacheGate.RunAsync(LockAsync);

        //==============================================

        Task<(bool, PhoneNumberDto?)> LockAsync()
            => Task.FromResult((this._cache.TryGetValue(number, out var result), result));
    }

    private async Task PeriodicUpdateAsync()
    {
        this._logger.LogInformation("Periodic update started");

        while (true)
        {
            try
            {
                await UpdateAsync().ConfigureAwait(false);
                await Task.Delay(this._updateInterval, this._applicationStopping);
            }
            catch (TaskCanceledException) { }
            catch (Exception ex)
            {
                Debug.Fail("Check what append");
                this._logger.LogWarning(ex, "Unexpected error durring Phonebook update");
            }

            if (this._applicationStopping.IsCancellationRequested)
            {
                this._logger.LogInformation("Periodic update stopped");
                return;
            }
        }
    }

    private async Task UpdateAsync()
    {
        if (!this._phonebookUpToDate)
            this._logger.LogInformation("Phonebook updating...");

        var phoneNumbers = await GetPhoneNumbersAsync().ConfigureAwait(false);
        if (phoneNumbers is null)
        {
            this._logger.LogWarning("Phonebook updating FAILED");
            this._phonebookUpToDate = false;
            return;
        }

        var cache = phoneNumbers.ToDictionary(pn => PhoneNumber.Parse(pn.UPhoneNumber));

        await this._cacheGate.RunAsync(LockAsync, this._applicationStopping)
            .ConfigureAwait(false);

        if (!this._phonebookUpToDate)
            this._logger.LogInformation("Phonebook updating DONE");
        this._phonebookUpToDate = true;

        //===================================================

        Task LockAsync()
        {
            this._cache = cache;
            return Task.CompletedTask;
        }
    }

    private async Task<IEnumerable<PhoneNumberDto>> GetPhoneNumbersAsync()
    {
        try
        {
            RestApiClient client = this._restApiClient;

            var uri = new Uri($"{this._webApiBaseUri}/PhoneNumbers");
            using var response = await client.SendJsonRequest(HttpMethod.Get, uri, null);
            response.EnsureSuccessStatusCode();
            var pageQueryResult = await response.DeseriaseJsonResponseAsync<PagedQueryResultDtoBase<PhoneNumberDto>>();
            var phoneNumbers = pageQueryResult.Results;

            return phoneNumbers;
        }
        catch (Exception ex)
        {
            Debug.Fail("Check what append");
            this._logger.LogWarning(ex, "Fail to get numbers from WebApi");
            return null;
        }

    }
}