using System.IO;
using System.IO.Ports;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Alphonse.Listener;

public class ConsoleHostedService : IHostedService
{
    private readonly ILogger _logger;
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly IServiceProvider _serviceProvider;

    private bool _isSelfStopping;
    private int? _exitCode;

    public ConsoleHostedService(
        ILogger<ConsoleHostedService> logger,
        IHostApplicationLifetime appLifetime,
        IServiceProvider serviceProvider)
    {
        this._logger = logger;
        this._appLifetime = appLifetime;
        this._serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var stoppingSource = CancellationTokenSource.CreateLinkedTokenSource(
            cancellationToken,
            this._appLifetime.ApplicationStopping);
        var stoppingToken = stoppingSource.Token;

        this._appLifetime.ApplicationStarted.Register(Run);

        return Task.CompletedTask;

        //=============================================================

        void Run() => Task.Run(() => RunAsync());

        async Task RunAsync()
        {
            try
            {
                var runner = this._serviceProvider.GetService<IConsoleRunner>();
                if (runner is null)
                    throw new InvalidOperationException("No runner found for ConcoleHostService");

                await runner.RunAsync(stoppingToken).ConfigureAwait(false);
                this._exitCode = 0;
            }
            catch (TaskCanceledException ex)
            {
                this._logger.LogWarning(ex, "Task was canceled!");
                this._exitCode = 89;
            }
            catch (Exception ex)
            {
                this._logger.LogCritical(ex, "Unhandled exception!");
                this._exitCode = 99;
            }
            finally
            {
                this._isSelfStopping = true;
                this._appLifetime.StopApplication();
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        if(!this._isSelfStopping)
        {
            this._logger.LogInformation("Good bye cruel world!");
        }

        // Exit code may be null if the user cancelled via Ctrl+C/SIGTERM
        Environment.ExitCode = this._exitCode.GetValueOrDefault(-1);

        return Task.CompletedTask;
    }
}