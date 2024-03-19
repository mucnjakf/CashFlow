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

internal sealed class GetTransactionQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetTransactionQuery, TransactionDto>
{
    public async Task<TransactionDto> Handle(GetTransactionQuery query, CancellationToken cancellationToken)
    {
        Transaction? transaction = await dbContext.Transactions
            .Include(x => x.Account)
            .Include(x => x.Category)
            .SingleOrDefaultAsync(x => x.Id == query.TransactionId, cancellationToken);

        if (transaction is null)
        {
            throw new TransactionException(HttpStatusCode.NotFound, Errors.Transaction.TransactionNotFound);
        }

        return transaction.ToTransactionDto();
    }
}