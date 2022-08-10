using System.Linq.Expressions;
using Alphonse.WebApi.Dto;
using Microsoft.EntityFrameworkCore;

namespace System.Linq;

public static class QuerableExtensions
{
    public static TResult ToPagedResult<TDbo, TItem, TOrderingKey, TResultContext, TResult>(this IQueryable<TDbo> dbQuery,
        int pageIndex, int pageSize, Expression<Func<TDbo, TOrderingKey>> orderingKeySelector,
        Func<TDbo, TItem> itemMapping,
        Func<IPagedQueryResultContext<TItem>, TResult> resultFBuilder)
        where TResult : PagedQueryResultDtoBase<TItem>
    {
        _ = dbQuery ?? throw new ArgumentNullException(nameof(dbQuery));
        // _ = orderingKeySelector ?? throw new ArgumentNullException(nameof(orderingKeySelector))
        _ = itemMapping ?? throw new ArgumentNullException(nameof(itemMapping));
        _ = resultFBuilder ?? throw new ArgumentNullException(nameof(resultFBuilder));

        if (pageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageIndex));
        if (pageSize < 0) throw new ArgumentOutOfRangeException(nameof(pageSize));

        //=== get total items===
        var nbTotalItems = dbQuery.Count(); // QUERY TAG

        //=== get paged items ====
        if(orderingKeySelector is not null)
            dbQuery = dbQuery.OrderBy(orderingKeySelector);
            
        var skippedItems = pageIndex * pageSize;
        dbQuery = dbQuery
            .Skip(skippedItems)
            .Take(pageSize);

        var entities = dbQuery.ToList(); // QUERY TAG

        //=== builup result ===
        var items = entities.Select(itemMapping).ToList();
        var resultContext = new PagedQueryResultContext<TItem>
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            NbTotalItems = nbTotalItems,
            Items = items,
        };

        var result = resultFBuilder(resultContext);

        return result;
    }

    public static async Task<TResult> ToPagedResultAsync<TDbo, TItem, TOrderingKey, TResultContext, TResult>(this IQueryable<TDbo> dbQuery,
        int pageIndex, int pageSize, Expression<Func<TDbo, TOrderingKey>> orderingKeySelector,
        Func<TDbo, TItem> itemMapping,
        Func<IPagedQueryResultContext<TItem>, TResult> resultFBuilder)
        where TResult : PagedQueryResultDtoBase<TItem>
    {
        _ = dbQuery ?? throw new ArgumentNullException(nameof(dbQuery));
        _ = orderingKeySelector ?? throw new ArgumentNullException(nameof(orderingKeySelector));
        _ = itemMapping ?? throw new ArgumentNullException(nameof(itemMapping));
        _ = resultFBuilder ?? throw new ArgumentNullException(nameof(resultFBuilder));

        if (pageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageIndex));
        if (pageSize < 0) throw new ArgumentOutOfRangeException(nameof(pageSize));

        //=== get total items===
        var nbTotalItems = await dbQuery.CountAsync(); // QUERY TAG

        //=== get page ====
        var skippedItems = pageIndex * pageSize;
        dbQuery = dbQuery
            .OrderBy(orderingKeySelector)
            .Skip(skippedItems)
            .Take(pageSize);

        var entities = await dbQuery.ToListAsync(); // QUERY TAG

        //=== builup result ===
        var items = entities.Select(itemMapping).ToList();
        var resultContext = new PagedQueryResultContext<TItem>
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            NbTotalItems = nbTotalItems,
            Items = items,
        };

        var result = resultFBuilder(resultContext);

        return result;
    }

    //========================================================================================

    private record PagedQueryResultContext<TItem> : IPagedQueryResultContext<TItem>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int NbTotalItems { get; set; }
        public ICollection<TItem> Items { get; set; } = null!;
    }
}

public interface IPagedQueryResultContext<TItem>
{
    public int PageIndex { get; }
    public int PageSize { get; }
    public int NbTotalItems { get; }
    public ICollection<TItem> Items { get; }
}