namespace CashFlow.Web.Dtos;

internal sealed record CategoryDto(Guid Id, string Name, int TransactionsCount, int IncomeCount, int ExpenseCount); // TODO: add to backend