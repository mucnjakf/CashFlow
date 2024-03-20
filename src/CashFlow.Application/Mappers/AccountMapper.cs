using CashFlow.Application.Dtos;
using CashFlow.Core.Entities;

namespace CashFlow.Application.Mappers;

/// <summary>
/// Account mapper
/// </summary>
public static class AccountMapper
{
    /// <summary>
    /// Maps account entity to account DTO
    /// </summary>
    /// <param name="account"><see cref="Account"/></param>
    /// <returns><see cref="AccountDto"/></returns>
    public static AccountDto ToAccountDto(this Account account)
    {
        return new AccountDto(
            account.Id,
            account.Balance,
            account.Transactions?.Count ?? 0);
    }

    /// <summary>
    /// Maps account entity to transaction account DTO
    /// </summary>
    /// <param name="account"><see cref="Account"/></param>
    /// <returns><see cref="TransactionAccountDto"/></returns>
    public static TransactionAccountDto ToTransactionAccountDto(this Account account)
    {
        return new TransactionAccountDto(account.Id, account.Balance);
    }
}