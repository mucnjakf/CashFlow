using MediatR;

namespace CashFlow.Application.Commands;

/// <summary>
/// Update transaction command
/// </summary>
/// <param name="Id">Transaction ID</param>
/// <param name="DateTimeUtc">Date and time of a transaction</param>
/// <param name="Description">Transaction description</param>
/// <param name="CategoryId">Transaction category</param>
public sealed record UpdateTransactionCommand(Guid Id, DateTime DateTimeUtc, string Description, Guid CategoryId) : IRequest;