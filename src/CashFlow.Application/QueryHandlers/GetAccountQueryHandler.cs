using CashFlow.Application.Context;
using CashFlow.Application.Dtos;
using CashFlow.Application.Mappers;
using CashFlow.Application.Queries;
using CashFlow.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Application.QueryHandlers;

/// <summary>
/// Get account query handler
/// </summary>
/// <param name="dbContext"><see cref="IApplicationDbContext"/></param>
internal sealed class GetAccountQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetAccountQuery, AccountDto>
{
    /// <summary>
    /// Handles getting account
    /// </summary>
    /// <param name="query"><see cref="GetAccountQuery"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="AccountDto"/></returns>
    public async Task<AccountDto> Handle(GetAccountQuery query, CancellationToken cancellationToken)
    {
        Account? account = await dbContext.Accounts
            .Include(x => x.Transactions)!
            .ThenInclude(x => x.Category)
            .SingleOrDefaultAsync(cancellationToken);

        if (account is not null)
        {
            return account.ToAccountDto();
        }

        account = Account.Create();

        await dbContext.Accounts.AddAsync(account, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return account.ToAccountDto();
    }
}