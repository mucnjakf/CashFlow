using CashFlow.Application.Dtos;
using CashFlow.Application.Mappers;
using CashFlow.Application.Queries;
using CashFlow.Core.Entities;
using CashFlow.Database.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Application.Handlers.Queries;

internal sealed class GetAccountQueryHandler(ApplicationDbContext dbContext) : IRequestHandler<GetAccountQuery, AccountDto>
{
    public async Task<AccountDto> Handle(GetAccountQuery request, CancellationToken cancellationToken)
    {
        Account? account = await dbContext.Accounts.SingleOrDefaultAsync(cancellationToken);

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