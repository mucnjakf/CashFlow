using System.Net;
using CashFlow.Application.Commands;
using CashFlow.Core.Constants;
using CashFlow.Core.Entities;
using CashFlow.Core.Exceptions;
using CashFlow.Database.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Application.CommandHandlers;

internal sealed class DeleteTransactionCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<DeleteTransactionCommand>
{
    public async Task Handle(DeleteTransactionCommand command, CancellationToken cancellationToken)
    {
        Transaction? transaction = await dbContext.Transactions
            .SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (transaction is null)
        {
            throw new TransactionException(HttpStatusCode.NotFound, Errors.Transaction.TransactionNotFound);
        }

        Account? account = await dbContext.Accounts.SingleOrDefaultAsync(cancellationToken);

        if (account is null)
        {
            throw new AccountException(HttpStatusCode.NotFound, Errors.Account.AccountNotFound);
        }

        account.RevertBalance(transaction.Type, transaction.Amount);

        dbContext.Transactions.Remove(transaction);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}