using MediatR;

namespace CashFlow.Application.Commands;

public sealed record UpdateTransactionCommand(Guid Id, DateTime DateTimeUtc, string Description, Guid CategoryId) : IRequest;