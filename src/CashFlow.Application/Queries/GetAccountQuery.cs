using CashFlow.Application.Dtos;
using MediatR;

namespace CashFlow.Application.Queries;

/// <summary>
/// Get account query
/// </summary>
public sealed record GetAccountQuery : IRequest<AccountDto>;