namespace CashFlow.Web.Requests;

internal sealed record UpdateTransactionRequest(DateTime DateTimeUtc, string Description, Guid CategoryId);