using System.Net;
using CashFlow.Application.Context;
using CashFlow.Application.Dtos;
using CashFlow.Application.Mappers;
using CashFlow.Application.Queries;
using CashFlow.Core.Constants;
using CashFlow.Core.Entities;
using CashFlow.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Application.QueryHandlers;

/// <summary>
/// Get transaction query handler
/// </summary>
/// <param name="dbContext"><see cref="IApplicationDbContext"/></param>
internal sealed class GetTransactionQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetTransactionQuery, TransactionDto>
{
    /// <summary>
    /// Handle getting transaction
    /// </summary>
    /// <param name="query"><see cref="GetTransactionQuery"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="TransactionDto"/></returns>
    /// <exception cref="TransactionException">Transaction not found</exception>
    public async Task<TransactionDto> Handle(GetTransactionQuery query, CancellationToken cancellationToken)
    {
        Transaction? transaction = await dbContext.Transactions
            .Include(x => x.Account)
            .Include(x => x.Category)
            .SingleOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

        if (transaction is null)
        {
            throw new TransactionException(HttpStatusCode.NotFound, Errors.Transaction.TransactionNotFound);
        }

        return transaction.ToTransactionDto();
    }
}