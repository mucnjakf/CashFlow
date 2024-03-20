using CashFlow.Core.Enums;

namespace CashFlow.Application.Dtos;

/// <summary>
/// Transaction DTO
/// </summary>
/// <param name="Id">Transaction ID</param>
/// <param name="DateTimeUtc">Date and time of a transaction</param>
/// <param name="Description">Transaction description</param>
/// <param name="Amount">Transaction amount</param>
/// <param name="Type"><see cref="TransactionType"/></param>
/// <param name="Account"><see cref="TransactionAccountDto"/></param>
/// <param name="Category"><see cref="TransactionCategoryDto"/></param>
public sealed record TransactionDto(
    Guid Id,
    DateTime DateTimeUtc,
    string Description,
    double Amount,
    TransactionType Type,
    TransactionAccountDto? Account,
    TransactionCategoryDto? Category);