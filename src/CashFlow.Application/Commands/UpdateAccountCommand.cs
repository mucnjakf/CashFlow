using MediatR;

namespace CashFlow.Application.Commands;

public sealed record UpdateAccountCommand(double Balance) : IRequest;