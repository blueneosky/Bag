using Alphonse.Listener.Dto;

namespace Alphonse.Listener
{
    public interface IHistoryPhoneNumberService
    {
        Task<CallHistoryDto> RegisterIncommingCallAsync(CallHistoryDto callHistory, CancellationToken token);
        Task UpdateHistoryCallAsync(CallHistoryDto callHistory, CancellationToken token);
    }
}