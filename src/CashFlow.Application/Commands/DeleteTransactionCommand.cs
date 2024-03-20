using MediatR;

namespace CashFlow.Application.Commands;

/// <summary>
/// Delete transaction command
/// </summary>
/// <param name="Id">Transaction ID</param>
public sealed record DeleteTransactionCommand(Guid Id) : IRequest;