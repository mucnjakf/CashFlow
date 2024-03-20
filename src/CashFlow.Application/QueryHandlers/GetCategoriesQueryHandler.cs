using CashFlow.Application.Context;
using CashFlow.Application.Dtos;
using CashFlow.Application.Mappers;
using CashFlow.Application.Pagination;
using CashFlow.Application.Queries;
using CashFlow.Core.Entities;
using CashFlow.Core.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Application.QueryHandlers;

/// <summary>
/// Get categories query handler
/// </summary>
/// <param name="dbContext"><see cref="IApplicationDbContext"/></param>
internal sealed class GetCategoriesQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetCategoriesQuery, PagedList<CategoryDto>>
{
    /// <summary>
    /// Handles getting categories
    /// </summary>
    /// <param name="query"><see cref="GetCategoriesQuery"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="PagedList{T}"/></returns>
    public async Task<PagedList<CategoryDto>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        IQueryable<Category> queryable = dbContext.Categories
            .Include(x => x.Transactions)!
            .ThenInclude(x => x.Account);

        queryable = Search(query.SearchQuery, queryable);
        queryable = Sort(query.SortBy, queryable);

        IQueryable<CategoryDto> categories = queryable.Select(x => x.ToCategoryDto());

        return await PagedList<CategoryDto>.ToPagedListAsync(categories, query.PageNumber, query.PageSize);
    }

    /// <summary>
    /// Searches categories query
    /// </summary>
    /// <param name="searchQuery">Search query for filtering</param>
    /// <param name="queryable">Categories query</param>
    /// <returns><see cref="IQueryable{T}"/></returns>
    private static IQueryable<Category> Search(string? searchQuery, IQueryable<Category> queryable)
    {
        if (string.IsNullOrWhiteSpace(searchQuery))
        {
            return queryable;
        }

        string capitalSearchQuery = searchQuery.ToUpper();

        return queryable.Where(x => x.Name.ToUpper().Contains(capitalSearchQuery));
    }

    /// <summary>
    /// Sorts categories query
    /// </summary>
    /// <param name="sortBy"><see cref="CategorySortBy"/></param>
    /// <param name="queryable">Categories query</param>
    /// <returns><see cref="IQueryable{T}"/></returns>
    private static IQueryable<Category> Sort(CategorySortBy? sortBy, IQueryable<Category> queryable) => sortBy switch
    {
        CategorySortBy.CreatedUtcAsc => queryable.OrderBy(x => x.CreatedUtc),
        CategorySortBy.CreatedUtcDesc => queryable.OrderByDescending(x => x.CreatedUtc),
        CategorySortBy.NameAsc => queryable.OrderBy(x => x.Name),
        CategorySortBy.NameDesc => queryable.OrderByDescending(x => x.Name),
        _ => queryable.OrderBy(x => x.CreatedUtc)
    };
}