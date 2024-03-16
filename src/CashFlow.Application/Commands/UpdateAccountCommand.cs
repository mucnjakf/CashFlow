using MediatR;

namespace CashFlow.Application.Commands;

// TODO: fluent validation
public sealed record UpdateAccountCommand(double Balance) : IRequest;