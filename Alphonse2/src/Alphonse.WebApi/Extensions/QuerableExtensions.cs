using System.Linq.Expressions;
using Alphonse.WebApi.Dto;
using Microsoft.EntityFrameworkCore;

namespace System.Linq;

public static class QuerableExtensions
{
    public static TResult ToPagedResult<TDbo, TItem, TOrderingKey, TResult>(
        this IQueryable<TDbo> dbQuery,
        int? pageIndex, int? pageSize,
        Expression<Func<TDbo, TOrderingKey>> orderingKeySelector,
        bool? orderByDescending,
        Func<TDbo, TItem> itemMapping,
        Func<IPagedQueryResultContext<TItem>, TResult> resultBuilder)
    {
        _ = dbQuery ?? throw new ArgumentNullException(nameof(dbQuery));
        _ = orderingKeySelector ?? throw new ArgumentNullException(nameof(orderingKeySelector));
        _ = itemMapping ?? throw new ArgumentNullException(nameof(itemMapping));
        _ = resultBuilder ?? throw new ArgumentNullException(nameof(resultBuilder));

        if (pageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageIndex));
        if (pageSize < 0) throw new ArgumentOutOfRangeException(nameof(pageSize));

        //=== get total items===
        var nbTotalItems = dbQuery.Count(); // QUERY TAG

        //=== get paged items ====
        if (orderByDescending.HasValue)
        {
            dbQuery = orderByDescending.Value
                ? dbQuery.OrderByDescending(orderingKeySelector)
                : dbQuery.OrderBy(orderingKeySelector);
        }

        if (pageSize.HasValue)
        {
            pageIndex ??= 0;
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

        var entities = dbQuery.ToList(); // QUERY TAG

        //=== buildup result ===
        var items = entities.Select(itemMapping).ToList();
        var resultContext = new PagedQueryResultContext<TItem>
        {
            PageIndex = pageIndex.Value,
            PageSize = pageSize.Value,
            NbTotalItems = nbTotalItems,
            Items = items,
        };

        var result = resultBuilder(resultContext);

        return result;
    }

    public static async Task<TResult> ToPagedResultAsync<TDbo, TItem, TOrderingKey, TResult>(
        this IQueryable<TDbo> dbQuery,
        int? pageIndex, int? pageSize,
        Expression<Func<TDbo, TOrderingKey>> orderingKeySelector,
        bool? orderByDescending,
        Func<TDbo, TItem> itemMapping,
        Func<IPagedQueryResultContext<TItem>, TResult> resultBuilder)
    {
        _ = dbQuery ?? throw new ArgumentNullException(nameof(dbQuery));
        _ = orderingKeySelector ?? throw new ArgumentNullException(nameof(orderingKeySelector));
        _ = itemMapping ?? throw new ArgumentNullException(nameof(itemMapping));
        _ = resultBuilder ?? throw new ArgumentNullException(nameof(resultBuilder));

        if (pageIndex < 0) throw new ArgumentOutOfRangeException(nameof(pageIndex));
        if (pageSize < 0) throw new ArgumentOutOfRangeException(nameof(pageSize));

        //=== get total items===
        var nbTotalItems = await dbQuery.CountAsync(); // QUERY TAG

        //=== get paged items ====
        if (orderByDescending.HasValue)
        {
            dbQuery = orderByDescending.Value
                ? dbQuery.OrderByDescending(orderingKeySelector)
                : dbQuery.OrderBy(orderingKeySelector);
        }

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

        var entities = await dbQuery.ToListAsync(); // QUERY TAG

        //=== buildup result ===
        var items = entities.Select(itemMapping).ToList();
        var resultContext = new PagedQueryResultContext<TItem>
        {
            PageIndex = pageIndex.Value,
            PageSize = pageSize.Value,
            NbTotalItems = nbTotalItems,
            Items = items,
        };

        var result = resultBuilder(resultContext);

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