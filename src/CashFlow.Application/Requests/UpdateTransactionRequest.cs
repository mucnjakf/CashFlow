namespace CashFlow.Application.Requests;

public sealed record UpdateTransactionRequest(DateTime DateTimeUtc, string Description, Guid CategoryId);