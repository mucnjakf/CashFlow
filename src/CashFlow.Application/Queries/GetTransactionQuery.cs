using CashFlow.Application.Dtos;
using MediatR;

namespace CashFlow.Application.Queries;

/// <summary>
/// Get transaction query
/// </summary>
/// <param name="Id">Transaction ID</param>
public sealed record GetTransactionQuery(Guid Id) : IRequest<TransactionDto>;