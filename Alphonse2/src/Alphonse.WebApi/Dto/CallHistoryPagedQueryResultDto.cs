namespace Alphonse.WebApi.Dto;

public class CallHistoryPagedQueryResultDto : PagedQueryResultDtoBase<CallHistoryDto>
{
    public CallHistoryPagedQueryResultDto() { }

    public CallHistoryPagedQueryResultDto(IPagedQueryResultContext<CallHistoryDto> context)
        : base(context) { }
}