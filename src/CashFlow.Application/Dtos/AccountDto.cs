namespace CashFlow.Application.Dtos;

public sealed record AccountDto(Guid Id, double Balance, int TransactionsCount);