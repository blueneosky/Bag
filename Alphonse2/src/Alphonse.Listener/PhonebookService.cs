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
    private readonly RestApiClient _restApiClient;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly string _webAppBaseUri;
    private readonly TimeSpan _updateInterval;
    private readonly CancellationToken _applicationStopping;

    private readonly AsyncLock _cacheGate = new AsyncLock();
    private Dictionary<PhoneNumber, PhoneNumberDto> _cache = new();

    public PhonebookService(
        ILogger<PhonebookService> logger,
        IOptions<AlphonseSettings> alphonseSettings,
        RestApiClient restApiClient,
        IHostApplicationLifetime appLifetime)
    {
        this._logger = logger;
        this._restApiClient = restApiClient;
        this._appLifetime = appLifetime;

        this._webAppBaseUri = alphonseSettings.Value.WebAppBaseUri;
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
        this._logger.LogInformation("Phonebook updating...");

        var phoneNumbers = await GetPhoneNumbersAsync().ConfigureAwait(false);
        if (phoneNumbers is null)
        {
            this._logger.LogWarning("Phonebook updating FAILED");
            return;
        }

        var cache = phoneNumbers.ToDictionary(pn => PhoneNumber.Parse(pn.UPhoneNumber));

        await this._cacheGate.RunAsync(LockAsync, this._applicationStopping)
            .ConfigureAwait(false);

        this._logger.LogInformation("Phonebook updating DONE");

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
            var uri = new Uri($"{this._webAppBaseUri}/PhoneNumbers");
            using var response = await this._restApiClient.SendJsonRequest(HttpMethod.Get, uri, null);
            response.EnsureSuccessStatusCode();
            var phoneNumbers = await response.DeseriaseJsonResponseAsync<PhoneNumberDto[]>();

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