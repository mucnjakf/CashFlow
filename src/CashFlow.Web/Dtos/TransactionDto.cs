using CashFlow.Web.Enums;

namespace CashFlow.Web.Dtos;

internal sealed record TransactionDto(
    Guid Id,
    DateTime DateTimeUtc,
    string Description,
    double Amount,
    TransactionType Type,
    TransactionAccountDto? Account,
    TransactionCategoryDto? Category);