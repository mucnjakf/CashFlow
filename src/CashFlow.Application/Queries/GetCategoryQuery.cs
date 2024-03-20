using CashFlow.Application.Dtos;
using MediatR;

namespace CashFlow.Application.Queries;

/// <summary>
/// Get category query
/// </summary>
/// <param name="Id">Category ID</param>
public sealed record GetCategoryQuery(Guid Id) : IRequest<CategoryDto>;