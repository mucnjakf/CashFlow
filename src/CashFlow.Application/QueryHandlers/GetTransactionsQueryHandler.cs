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
/// Get transactions query handler
/// </summary>
/// <param name="dbContext"><see cref="IApplicationDbContext"/></param>
internal sealed class GetTransactionsQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetTransactionsQuery, PagedList<TransactionDto>>
{
    /// <summary>
    /// Handles getting transactions
    /// </summary>
    /// <param name="query"><see cref="GetTransactionsQuery"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="PagedList{T}"/></returns>
    public async Task<PagedList<TransactionDto>> Handle(GetTransactionsQuery query, CancellationToken cancellationToken)
    {
        IQueryable<Transaction> queryable = dbContext.Transactions
            .Include(x => x.Account)
            .Include(x => x.Category);

        queryable = Filter(query.Type, queryable);
        queryable = Search(query.SearchQuery, queryable);
        queryable = Sort(query.SortBy, queryable);

        IQueryable<TransactionDto> transactions = queryable.Select(x => x.ToTransactionDto());

        return await PagedList<TransactionDto>.ToPagedListAsync(transactions, query.PageNumber, query.PageSize);
    }

    /// <summary>
    /// Filters transactions query
    /// </summary>
    /// <param name="type"><see cref="TransactionType"/></param>
    /// <param name="queryable">Transactions query</param>
    /// <returns><see cref="IQueryable{T}"/></returns>
    private static IQueryable<Transaction> Filter(TransactionType? type, IQueryable<Transaction> queryable)
    {
        return type is null
            ? queryable
            : queryable.Where(x => x.Type == type);
    }

    /// <summary>
    /// Searches transactions query
    /// </summary>
    /// <param name="searchQuery">Search query for filtering</param>
    /// <param name="queryable">Transactions query</param>
    /// <returns><see cref="IQueryable{T}"/></returns>
    private static IQueryable<Transaction> Search(string? searchQuery, IQueryable<Transaction> queryable)
    {
        if (string.IsNullOrWhiteSpace(searchQuery))
        {
            return queryable;
        }

        string capitalSearchQuery = searchQuery.ToUpper();

        return queryable.Where(x =>
            x.Description.ToUpper().Contains(capitalSearchQuery) ||
            x.Amount.ToString().ToUpper().Contains(capitalSearchQuery) ||
            x.Category!.Name.ToUpper().Contains(capitalSearchQuery));
    }

    /// <summary>
    /// Sorts transactions query
    /// </summary>
    /// <param name="sortBy"><see cref="TransactionSortBy"/></param>
    /// <param name="queryable">Transactions query</param>
    /// <returns><see cref="IQueryable{T}"/></returns>
    private static IQueryable<Transaction> Sort(TransactionSortBy? sortBy, IQueryable<Transaction> queryable) => sortBy switch
    {
        TransactionSortBy.DateTimeUtcAsc => queryable.OrderBy(x => x.DateTimeUtc),
        TransactionSortBy.DateTimeUtcDesc => queryable.OrderByDescending(x => x.DateTimeUtc),
        TransactionSortBy.DescriptionAsc => queryable.OrderBy(x => x.Description),
        TransactionSortBy.DescriptionDesc => queryable.OrderByDescending(x => x.Description),
        TransactionSortBy.AmountAsc => queryable.OrderBy(x => x.Amount),
        TransactionSortBy.AmountDesc => queryable.OrderByDescending(x => x.Amount),
        _ => queryable.OrderBy(x => x.DateTimeUtc)
    };
}