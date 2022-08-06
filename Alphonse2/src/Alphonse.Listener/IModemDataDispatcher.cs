namespace Alphonse.Listener;

public interface IModemDataDispatcher
{
    Task DispatchAsync(string data, CancellationToken token);
}