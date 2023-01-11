namespace Alphonse.Listener.Connectors;

public interface IModemDataDispatcher
{
    Task DispatchAsync(ModemDataType dataType, string? data, CancellationToken token);
}