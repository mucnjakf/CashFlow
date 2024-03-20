using CashFlow.Application.Dtos;
using CashFlow.Core.Enums;
using MediatR;

namespace CashFlow.Application.Commands;

/// <summary>
/// Create transaction command
/// </summary>
/// <param name="DateTimeUtc">Date and time of a transaction</param>
/// <param name="Description">Transaction description</param>
/// <param name="Amount">Transaction amount</param>
/// <param name="Type"><see cref="TransactionType"/></param>
/// <param name="CategoryId">Transaction category</param>
public sealed record CreateTransactionCommand(
    DateTime DateTimeUtc,
    string Description,
    double Amount,
    TransactionType Type,
    Guid CategoryId) : IRequest<TransactionDto>;