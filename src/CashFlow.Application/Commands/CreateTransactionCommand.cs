using CashFlow.Application.Dtos;
using CashFlow.Core.Enums;
using MediatR;

namespace CashFlow.Application.Commands;

// TODO: fluent validation
public sealed record CreateTransactionCommand(
    DateTime DateTimeUtc,
    string Description,
    double Amount,
    TransactionType Type,
    Guid CategoryId) : IRequest<TransactionDto>;