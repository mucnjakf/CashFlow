using CashFlow.Application.Dtos;
using MediatR;

namespace CashFlow.Application.Queries;

public sealed record GetCategoryQuery(Guid Id) : IRequest<CategoryDto>;