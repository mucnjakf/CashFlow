namespace CashFlow.Application.Dtos;

public sealed record CategoryDto(Guid Id, string Name, int TransactionsCount);