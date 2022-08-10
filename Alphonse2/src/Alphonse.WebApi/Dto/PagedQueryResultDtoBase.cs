namespace Alphonse.WebApi.Dto;

public abstract class PagedQueryResultDtoBase<TItem>
{
    protected PagedQueryResultDtoBase() { }

    protected PagedQueryResultDtoBase(IPagedQueryResultContext<TItem> context)
    {
        this.PageIndex = context.PageIndex;
        this.PageSize = context.PageSize;
        this.NbTotalResults = context.NbTotalItems;
        this.Results = context.Items;

        // === computed ===
        this.NbTotalPage = (int)Math.Ceiling((double)context.NbTotalItems / context.PageSize);
    }

    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int NbTotalPage { get; set; }
    public int NbTotalResults { get; set; }
    public ICollection<TItem> Results { get; set; } = new TItem[0];
}