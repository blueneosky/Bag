namespace Alphonse.WebApi.Dto;

public class PageQueryParams : IPageQueryParams
{
    public int? PageSize { get; set; }
    public int? PageIndex { get; set; }
    public int? ReverseOrder { get; set; }
    public string? SearchPattern { get; set; }
}

public class CallHistoryPageQueryParams : PageQueryParams
{
    public DateTime? After { get; set; }
    public DateTime? Before { get; set; }
}