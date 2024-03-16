using CashFlow.Application.Dtos;
using CashFlow.Application.Mappers;
using CashFlow.Application.Queries;
using CashFlow.Core.Constants;
using CashFlow.Core.Entities;
using CashFlow.Core.Exceptions;
using CashFlow.Database.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Application.Handlers;

internal sealed class GetTransactionQueryHandler(ApplicationDbContext dbContext) : IRequestHandler<GetTransactionQuery, TransactionDto>
{
    public async Task<TransactionDto> Handle(GetTransactionQuery query, CancellationToken cancellationToken)
    {
        Transaction? transaction = await dbContext.Transactions
            .Include(x => x.Account)
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == query.TransactionId, cancellationToken);

        if (transaction is null)
        {
            throw new TransactionException(Errors.Transaction.TransactionNotFound);
        }

        return transaction.ToTransactionDto();
    }
}