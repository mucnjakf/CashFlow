namespace CashFlow.Application.Dtos;

/// <summary>
/// Category DTO
/// </summary>
/// <param name="Id">Category ID</param>
/// <param name="Name">Category name</param>
/// <param name="TransactionsCount">Number of category transactions</param>
public sealed record CategoryDto(Guid Id, string Name, int TransactionsCount);