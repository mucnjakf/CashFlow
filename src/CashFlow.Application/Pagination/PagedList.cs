using Microsoft.EntityFrameworkCore;

namespace CashFlow.Application.Pagination;

/// <summary>
/// Paged list for pagination
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public sealed class PagedList<T>
{
    public IEnumerable<T> Items { get; private set; }

    public int PageNumber { get; private set; }

    public int PageSize { get; private set; }

    public int TotalPages { get; private set; }

    public int TotalCount { get; private set; }

    public bool HasPrevious => PageNumber > 1;

    public bool HasNext => PageNumber < TotalPages;

    private PagedList(IEnumerable<T> items, int pageNumber, int pageSize, int totalCount)
    {
        Items = items;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        TotalCount = totalCount;
    }

    /// <summary>
    /// Converts query to paged list
    /// </summary>
    /// <param name="source"><see cref="IQueryable{T}"/></param>
    /// <param name="pageNumber">Pagination page number</param>
    /// <param name="pageSize">Pagination page size</param>
    /// <returns><see cref="PagedList{T}"/></returns>
    public static async Task<PagedList<T>> ToPagedListAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        int totalCount = source.Count();
        List<T> items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedList<T>(items, pageNumber, pageSize, totalCount);
    }
}