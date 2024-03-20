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
/// Update account command handler
/// </summary>
/// <param name="dbContext"><see cref="IApplicationDbContext"/></param>
internal sealed class UpdateAccountCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<UpdateAccountCommand>
{
    /// <summary>
    /// Handles updating account
    /// </summary>
    /// <param name="command"><see cref="UpdateAccountCommand"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <exception cref="AccountException">Account not found</exception>
    public async Task Handle(UpdateAccountCommand command, CancellationToken cancellationToken)
    {
        Account? account = await dbContext.Accounts.SingleOrDefaultAsync(cancellationToken);

        if (account is null)
        {
            throw new AccountException(HttpStatusCode.NotFound, Errors.Account.AccountNotFound);
        }

        account.Update(command.Balance);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}