using CashFlow.Application.Dtos;
using CashFlow.Core.Entities;

namespace CashFlow.Application.Mappers;

public static class TransactionMapper
{
    public static AccountTransactionDto ToAccountTransactionDto(this Transaction transaction)
    {
        return new AccountTransactionDto(
            transaction.Id,
            transaction.DateTimeUtc,
            transaction.Description,
            transaction.Amount,
            transaction.Type,
            transaction.Category.ToTransactionCategoryDto());
    }
}