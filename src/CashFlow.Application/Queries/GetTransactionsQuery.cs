using CashFlow.Application.Dtos;
using CashFlow.Application.Pagination;
using CashFlow.Core.Enums;
using MediatR;

namespace CashFlow.Application.Queries;

/// <summary>
/// Get transactions query
/// </summary>
/// <param name="PageNumber">Pagination page number</param>
/// <param name="PageSize">Pagination page size</param>
/// <param name="Type"><see cref="TransactionType"/></param>
/// <param name="SortBy"><see cref="TransactionSortBy"/></param>
/// <param name="SearchQuery">Search query for filtering</param>
public sealed record GetTransactionsQuery(
    int PageNumber,
    int PageSize,
    TransactionType? Type,
    TransactionSortBy? SortBy,
    string? SearchQuery) : IRequest<PagedList<TransactionDto>>;