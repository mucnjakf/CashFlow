using CashFlow.Application.Dtos;
using MediatR;

namespace CashFlow.Application.Commands;

public sealed record CreateCategoryCommand(string Name) : IRequest<CategoryDto>;