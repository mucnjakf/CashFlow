using CashFlow.Application.Commands;
using CashFlow.Application.Dtos;
using CashFlow.Application.Mappers;
using CashFlow.Core.Constants;
using CashFlow.Core.Entities;
using CashFlow.Core.Exceptions;
using CashFlow.Database.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Application.CommandHandlers;

internal sealed class CreateTransactionCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<CreateTransactionCommand, TransactionDto>
{
    public async Task<TransactionDto> Handle(CreateTransactionCommand command, CancellationToken cancellationToken)
    {
        Account? account = await dbContext.Accounts.SingleOrDefaultAsync(cancellationToken);

        if (account is null)
        {
            throw new AccountException(Errors.Account.AccountNotFound);
        }

        Transaction transaction = Transaction.Create(
            command.DateTimeUtc,
            command.Description,
            command.Amount,
            command.Type,
            account.Id,
            command.CategoryId);

        await dbContext.Transactions.AddAsync(transaction, cancellationToken);

        account.UpdateBalance(transaction.Type, transaction.Amount);

        await dbContext.SaveChangesAsync(cancellationToken);

        Transaction? newTransaction = await dbContext.Transactions
            .Include(x => x.Account)
            .Include(x => x.Category)
            .SingleOrDefaultAsync(x => x.Id == transaction.Id, cancellationToken);

        return newTransaction!.ToTransactionDto();
    }
}