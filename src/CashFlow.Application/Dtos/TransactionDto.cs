using CashFlow.Core.Enums;

namespace CashFlow.Application.Dtos;

public sealed record TransactionDto(
    Guid Id,
    DateTime DateTimeUtc,
    string Description,
    double Amount,
    TransactionType Type,
    TransactionCategoryDto Category);