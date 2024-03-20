using MediatR;

namespace CashFlow.Application.Commands;

/// <summary>
/// Update account command
/// </summary>
/// <param name="Balance">Account balance</param>
public sealed record UpdateAccountCommand(double Balance) : IRequest;