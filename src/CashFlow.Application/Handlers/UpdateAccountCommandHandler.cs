using CashFlow.Application.Commands;
using CashFlow.Core.Constants;
using CashFlow.Core.Entities;
using CashFlow.Core.Exceptions;
using CashFlow.Database.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Application.Handlers;

internal sealed class UpdateAccountCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<UpdateAccountCommand>
{
    public async Task Handle(UpdateAccountCommand command, CancellationToken cancellationToken)
    {
        Account? account = await dbContext.Accounts.SingleOrDefaultAsync(cancellationToken);

        if (account is null)
        {
            throw new AccountException(Errors.Account.AccountNotFound);
        }

        account.Update(command.Balance);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}