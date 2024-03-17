using CashFlow.Application.Dtos;
using CashFlow.Application.Pagination;
using CashFlow.Core.Enums;
using MediatR;

namespace CashFlow.Application.Queries;

public sealed record GetCategoriesQuery(
    int PageNumber,
    int PageSize,
    CategorySortBy? SortBy,
    string? SearchQuery) : IRequest<PagedList<CategoryDto>>;