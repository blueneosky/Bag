using System.Linq.Expressions;
using Alphonse.WebApi.Extensions;
using Microsoft.EntityFrameworkCore;

namespace System.Linq;

public static class QuerableExtensions
{
    public static async Task<IPagedQueryResultContext<TItem>> ToPagedResultAsync<TDbo, TItem>(
        this IQueryable<TDbo> dbQuery,
        IPageQueryParams pageQueryParams,
        Expression<Func<TDbo, string, bool>>? searchFilterPredicate,
        Func<TDbo, TItem> itemMapping,
        Expression<Func<TDbo, object>> orderByKeySelector,
        params Expression<Func<TDbo, object>>[] thenByKeySelectors)
    {
        _ = dbQuery ?? throw new ArgumentNullException(nameof(dbQuery));
        _ = orderByKeySelector ?? throw new ArgumentNullException(nameof(orderByKeySelector));
        _ = itemMapping ?? throw new ArgumentNullException(nameof(itemMapping));

        if (pageQueryParams.PageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageQueryParams), $"{nameof(pageQueryParams.PageIndex)} must be greater or equal to 0");
        if (pageQueryParams.PageSize < 0) throw new ArgumentOutOfRangeException(nameof(pageQueryParams), $"{nameof(pageQueryParams.PageSize)} must be greater or equal to 0");

        //=== apply search===
        if (!string.IsNullOrEmpty(pageQueryParams.SearchPattern)
            && searchFilterPredicate != null)
        {
            var wherePredicate = searchFilterPredicate.Substitute<Func<TDbo, bool>, string>(1, () => pageQueryParams.SearchPattern);
            dbQuery = dbQuery.Where(wherePredicate);
        }

        //=== get total items===
        var nbTotalItems = await dbQuery.CountAsync(); // QUERY TAG

        //=== get paged items ====
        var orderByDescending = pageQueryParams.ReverseOrder.TernaryNullable(() => (bool?)null, v => v != 0);
        if (orderByDescending.HasValue)
        {
            var isAscending = !orderByDescending.Value;
            var orderedQuery = isAscending ? dbQuery.OrderBy(orderByKeySelector) : dbQuery.OrderByDescending(orderByKeySelector);
            dbQuery = thenByKeySelectors
                .Where(ks => ks != null)
                .Aggregate(
                    orderedQuery,
                    (oq, oks) => isAscending ? oq.ThenBy(oks) : oq.ThenByDescending(oks));
        }

        var pageIndex = pageQueryParams.PageIndex;
        var pageSize = pageQueryParams.PageSize;
        if (pageIndex.HasValue && pageSize.HasValue)
        {
            var skippedItems = pageIndex.Value * pageSize.Value;
            dbQuery = dbQuery
                .Skip(skippedItems)
                .Take(pageSize.Value);
        }
        else
        {
            pageIndex = 0;
            pageSize = nbTotalItems;
        }

        //=== buildup result ===
        var entities = await dbQuery.ToListAsync(); // QUERY TAG

        var items = entities.Select(itemMapping).ToList();
        var result = new PagedQueryResultContext<TItem>
        {
            PageIndex = pageIndex.Value,
            PageSize = pageSize.Value,
            NbTotalItems = nbTotalItems,
            SearchPattern = pageQueryParams.SearchPattern,
            Items = items,
        };

        return result;
    }

    //========================================================================================

    private record PagedQueryResultContext<TItem> : IPagedQueryResultContext<TItem>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int NbTotalItems { get; set; }
        public string? SearchPattern { get; set; }
        public ICollection<TItem> Items { get; set; } = null!;
    }
}

public interface IPageQueryParams
{
    public int? PageSize { get; }
    public int? PageIndex { get; }
    public int? ReverseOrder { get; }
    public string? SearchPattern { get; }
}

public interface IPagedQueryResultContext<TItem>
{
    public int PageIndex { get; }
    public int PageSize { get; }
    public int NbTotalItems { get; }
    public string? SearchPattern { get; set; }
    public ICollection<TItem> Items { get; }
}