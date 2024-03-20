using CashFlow.Core.Enums;

namespace CashFlow.Application.Dtos;

/// <summary>
/// Category transaction DTO
/// </summary>
/// <param name="Id">Transaction ID</param>
/// <param name="DateTimeUtc">Date and time of a transaction</param>
/// <param name="Description">Transaction description</param>
/// <param name="Amount">Transaction amount</param>
/// <param name="Type"><see cref="TransactionType"/></param>
/// <param name="Account"><see cref="TransactionAccountDto"/></param>
public sealed record CategoryTransactionDto(
    Guid Id,
    DateTime DateTimeUtc,
    string Description,
    double Amount,
    TransactionType Type,
    TransactionAccountDto Account);