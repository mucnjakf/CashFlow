﻿using CashFlow.Application.Dtos;
using CashFlow.Core.Entities;

namespace CashFlow.Application.Mappers;

public static class TransactionMapper
{
    public static TransactionDto ToTransactionDto(this Transaction transaction)
    {
        return new TransactionDto(
            transaction.Id,
            transaction.DateTimeUtc,
            transaction.Description,
            transaction.Amount,
            transaction.Type,
            transaction.Account.ToTransactionAccountDto(),
            transaction.Category.ToTransactionCategoryDto());
    }

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

    public static CategoryTransactionDto ToCategoryTransactionDto(this Transaction transaction)
    {
        return new CategoryTransactionDto(
            transaction.Id,
            transaction.DateTimeUtc,
            transaction.Description,
            transaction.Amount,
            transaction.Type,
            transaction.Account.ToTransactionAccountDto());
    }
}