namespace Alphonse.Listener
{
    public interface IModem : IDisposable
    {
        void Open();
        
        void Close();

        Task ListenAsync(IModemDataDispatcher listener, CancellationToken token);

        Task PickupHangupAsync(TimeSpan hangupDelay, CancellationToken token);
    }
}