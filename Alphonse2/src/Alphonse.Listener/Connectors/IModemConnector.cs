namespace Alphonse.Listener.Connectors;

public interface IModemConnector : IDisposable
{
    Task OpenAsync();

    void Close();

    Task ListenAsync(IModemDataDispatcher listener, CancellationToken token);

    Task PickupHangupAsync(TimeSpan hangupDelay, CancellationToken token);

    Task WriteCommandsAsync(IEnumerable<string> commands, CancellationToken token);
}

public static class ModemConnectorExtensions
{
    public static Task WriteCommandAsync(this IModemConnector connector, string command, CancellationToken token)
        => connector.WriteCommandsAsync(new[] { command }, token);
}