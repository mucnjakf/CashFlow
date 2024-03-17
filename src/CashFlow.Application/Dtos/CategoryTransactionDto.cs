using CashFlow.Core.Enums;

namespace CashFlow.Application.Dtos;

public sealed record CategoryTransactionDto(
    Guid Id,
    DateTime DateTimeUtc,
    string Description,
    double Amount,
    TransactionType Type,
    TransactionAccountDto Account);