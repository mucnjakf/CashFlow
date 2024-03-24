namespace CashFlow.Web.Pagination;

internal sealed class PagedList<T>
{
    public IEnumerable<T> Items { get; set; } = default!;

    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public int TotalPages { get; set; }

    public int TotalCount { get; set; }

    public bool HasPrevious { get; set; }

    public bool HasNext { get; set; }
}