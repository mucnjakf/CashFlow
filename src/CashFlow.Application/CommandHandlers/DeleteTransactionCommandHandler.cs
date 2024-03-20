using System.Net;
using CashFlow.Application.Commands;
using CashFlow.Application.Context;
using CashFlow.Core.Constants;
using CashFlow.Core.Entities;
using CashFlow.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Application.CommandHandlers;

/// <summary>
/// Delete transaction command handler
/// </summary>
/// <param name="dbContext"><see cref="IApplicationDbContext"/></param>
internal sealed class DeleteTransactionCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<DeleteTransactionCommand>
{
    /// <summary>
    /// Handles deleting transaction
    /// </summary>
    /// <param name="command"><see cref="DeleteTransactionCommand"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <exception cref="TransactionException">Transaction not found</exception>
    /// <exception cref="AccountException">Account not found</exception>
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