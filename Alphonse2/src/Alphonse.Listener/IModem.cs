namespace Alphonse.Listener
{
    public interface IModem : IDisposable
    {
        void Open();
        
        void Close();

        Task ListenAsync(IModemDataDispatcher listener, CancellationToken token);

        void WriteCommands(IEnumerable<string> commands);

        Task PickupHangupAsync(TimeSpan hangupDelay, CancellationToken token);
    }

    public static class ModemExtensions
    {
        public static void WriteCommands(this IModem modem, params string[] commands)
            => modem?.WriteCommands((IEnumerable<string>)commands);
    }
}