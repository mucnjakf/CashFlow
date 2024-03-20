using CashFlow.Application.Dtos;
using CashFlow.Core.Entities;

namespace CashFlow.Application.Mappers;

/// <summary>
/// Transaction mapper
/// </summary>
public static class TransactionMapper
{
    /// <summary>
    /// Maps transaction entity to transaction DTO
    /// </summary>
    /// <param name="transaction"><see cref="Transaction"/></param>
    /// <returns><see cref="TransactionDto"/></returns>
    public static TransactionDto ToTransactionDto(this Transaction transaction)
    {
        return new TransactionDto(
            transaction.Id,
            transaction.DateTimeUtc,
            transaction.Description,
            transaction.Amount,
            transaction.Type,
            transaction.Account?.ToTransactionAccountDto(),
            transaction.Category?.ToTransactionCategoryDto());
    }
}