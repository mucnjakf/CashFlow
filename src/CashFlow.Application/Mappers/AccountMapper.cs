using CashFlow.Application.Dtos;
using CashFlow.Core.Entities;

namespace CashFlow.Application.Mappers;

public static class AccountMapper
{
    public static AccountDto ToAccountDto(this Account account)
    {
        return new AccountDto(
            account.Id,
            account.Balance,
            account.Transactions?.Count ?? 0);
    }

    public static TransactionAccountDto ToTransactionAccountDto(this Account account)
    {
        return new TransactionAccountDto(account.Id, account.Balance);
    }
}