using CashFlow.Application.Dtos;
using MediatR;

namespace CashFlow.Application.Commands;

// TODO: fluent validaiton
public sealed record CreateCategoryCommand(string Name) : IRequest<CategoryDto>;