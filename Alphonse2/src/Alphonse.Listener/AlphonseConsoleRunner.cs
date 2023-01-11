using Alphonse.Listener.Connectors;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Alphonse.Listener;

internal sealed class AlphonseConsoleRunner : BackgroundService
{
    private readonly ILogger _logger;
    private readonly IModemConnector _modemConnector;
    private readonly IModemDataDispatcher _listener;
    private readonly IOptions<AlphonseSettings> _settings;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly TimeSpan _resetTime;

    public AlphonseConsoleRunner(
        ILogger<AlphonseConsoleRunner> logger,
        IModemConnector modemConnector,
        IModemDataDispatcher listener,
        IOptions<AlphonseSettings> settings,
        IHostApplicationLifetime hostApplicationLifetime
        )
    {
        this._logger = logger;
        this._modemConnector = modemConnector;
        this._listener = listener;
        this._settings = settings;
        this._hostApplicationLifetime = hostApplicationLifetime;
        this._resetTime = settings.Value.AutoResetTime ?? DateTime.Now.TimeOfDay;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken) => Task.Run(async () =>
    {
        // aggregate signals
        using var globalStoppingTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
            this._hostApplicationLifetime.ApplicationStopping,
            stoppingToken);
        var globalStoppingToken = globalStoppingTokenSource.Token;

        while (!globalStoppingToken.IsCancellationRequested)
        {
            // run for an other 24h
            var remainingTime = GetRemainingTimeUntilNextResetTime();
            using var periodeSource = CancellationTokenSource.CreateLinkedTokenSource(globalStoppingToken);
            periodeSource.CancelAfter(remainingTime);
            this._logger.LogInformation("Next modem com reset setup for {Date}", DateTime.Now + remainingTime);

            await this.ListenAsync(periodeSource.Token).ConfigureAwait(false);
        }

        this._logger.LogInformation("Gracefull stopp");

        //==================================================================

        TimeSpan GetRemainingTimeUntilNextResetTime()
        {
            var now = DateTime.Now;
            var nextResetTime = now.Date + this._resetTime;
            while (nextResetTime < now)
            {
                nextResetTime = nextResetTime.AddDays(1);
            }

            // check that the remaining is not too small. Better not start/stop in a short time
            var remaining = nextResetTime - now;
            if (remaining < TimeSpan.FromMinutes(5))
                remaining += TimeSpan.FromDays(1);  // well, no, too short

            return remaining;
        }
    }, stoppingToken);

    private async Task ListenAsync(CancellationToken stoppingToken)
    {
        try
        {
            this._modemConnector.Close();
            await this._modemConnector.OpenAsync().ConfigureAwait(false);

            this._logger.LogDebug("Start listening...");
            await this._modemConnector.ListenAsync(this._listener, stoppingToken).ConfigureAwait(false);
            this._logger.LogDebug("Start listening DONE");
        }
        catch (TaskCanceledException) { }
        catch (Exception ex)
        {
            Debug.Fail($"Fail to connect: {ex}");
        }
        finally
        {
            this._modemConnector.Close();
        }
    }
}