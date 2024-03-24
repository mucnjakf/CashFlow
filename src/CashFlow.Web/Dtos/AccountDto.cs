namespace CashFlow.Web.Dtos;

internal sealed record AccountDto(Guid Id, double Balance, int TransactionsCount);