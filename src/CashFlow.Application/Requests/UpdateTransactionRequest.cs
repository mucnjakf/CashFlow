namespace CashFlow.Application.Requests;

/// <summary>
/// Update transaction request
/// </summary>
/// <param name="DateTimeUtc">Date and time of a transaction</param>
/// <param name="Description">Transaction description</param>
/// <param name="CategoryId">Transaction category</param>
public sealed record UpdateTransactionRequest(DateTime DateTimeUtc, string Description, Guid CategoryId);