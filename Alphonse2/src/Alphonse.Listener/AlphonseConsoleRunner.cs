using System.IO;
using System.IO.Ports;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Alphonse.Listener;

internal sealed class AlphonseConsoleRunner : IConsoleRunner
{
    private readonly ILogger _logger;
    private readonly Modem _modem;
    private readonly IModemDataDispatcher _listener;
    private readonly IOptions<AlphonseSettings> _settings;
    private readonly TimeSpan _resetTime;

    public AlphonseConsoleRunner(ILogger<AlphonseConsoleRunner> logger, Modem modem, IModemDataDispatcher listener, IOptions<AlphonseSettings> settings)
    {
        this._logger = logger;
        this._modem = modem;
        this._listener = listener;
        this._settings = settings;
        this._resetTime = settings.Value.AutoResetTime ?? DateTime.Now.TimeOfDay;
    }

    public async Task RunAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // run for an other 24h
            var remainingTime = GetRemainingTimeUntilNextResetTime();
            var periodeSource = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);
            periodeSource.CancelAfter(remainingTime);
            this._logger.LogInformation("Next modem com reset setup for {Date}", DateTime.Now + remainingTime);

            await this.ListenAsync(periodeSource.Token).ConfigureAwait(false);
        }

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
    }

    private async Task ListenAsync(CancellationToken token)
    {
        try
        {
            this._modem.Open();

            this._logger.LogDebug("Start listening...");
            await this._modem.ListenAsync(this._listener, token).ConfigureAwait(false);
            this._logger.LogDebug("Start listening DONE");
        }
        catch (TaskCanceledException) { }
        catch (Exception ex)
        {
            Debug.Fail($"Fail to connect: {ex}");
        }
        finally
        {
            this._modem.Close();
        }
    }
}