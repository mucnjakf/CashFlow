using CashFlow.Application.Dtos;
using CashFlow.Application.Pagination;
using CashFlow.Core.Enums;
using MediatR;

namespace CashFlow.Application.Queries;

/// <summary>
/// Get categories query
/// </summary>
/// <param name="PageNumber">Pagination page number</param>
/// <param name="PageSize">Pagination page size</param>
/// <param name="SortBy"><see cref="CategorySortBy"/></param>
/// <param name="SearchQuery">Search query for filtering</param>
public sealed record GetCategoriesQuery(
    int PageNumber,
    int PageSize,
    CategorySortBy? SortBy,
    string? SearchQuery) : IRequest<PagedList<CategoryDto>>;