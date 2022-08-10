using Alphonse.WebApi.Dbo;

namespace Alphonse.WebApi.Dto;

public class CallHistoryPagedQueryResultDto : PagedQueryResultDtoBase<CallHistoryDbo>
{
    public CallHistoryPagedQueryResultDto() { }

    public CallHistoryPagedQueryResultDto(IPagedQueryResultContext<CallHistoryDbo> context)
        : base(context) { }
}