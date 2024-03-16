using MediatR;

namespace CashFlow.Application.Commands;

public sealed record DeleteTransactionCommand(Guid Id) : IRequest;