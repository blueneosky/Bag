
namespace Alphonse.Listener;

public interface IConsoleRunner
{
    Task RunAsync(CancellationToken stoppingToken);
}