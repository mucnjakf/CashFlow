namespace CashFlow.Application.Dtos;

/// <summary>
/// Transaction account DTO
/// </summary>
/// <param name="Id">Account ID</param>
/// <param name="Balance">Account balance</param>
public sealed record TransactionAccountDto(Guid Id, double Balance);