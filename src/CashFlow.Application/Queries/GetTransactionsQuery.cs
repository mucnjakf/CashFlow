using CashFlow.Application.Dtos;
using CashFlow.Application.Pagination;
using CashFlow.Core.Enums;
using MediatR;

namespace CashFlow.Application.Queries;

public sealed record GetTransactionsQuery(
    int PageNumber,
    int PageSize,
    TransactionType? Type,
    TransactionSortBy? SortBy,
    string? SearchQuery) : IRequest<PagedList<TransactionDto>>;