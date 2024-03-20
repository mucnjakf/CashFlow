namespace CashFlow.Application.Dtos;

/// <summary>
/// Account DTO
/// </summary>
/// <param name="Id">Account ID</param>
/// <param name="Balance">Account balance</param>
/// <param name="TransactionsCount">Number of account transactions</param>
public sealed record AccountDto(Guid Id, double Balance, int TransactionsCount);