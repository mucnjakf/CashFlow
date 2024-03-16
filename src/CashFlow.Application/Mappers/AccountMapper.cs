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
            account.Transactions?.Select(x => x.ToAccountTransactionDto()));
    }

    public static TransactionAccountDto ToTransactionAccountDto(this Account account)
    {
        return new TransactionAccountDto(account.Id, account.Balance);
    }
}