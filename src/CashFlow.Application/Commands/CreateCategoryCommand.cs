using CashFlow.Application.Dtos;
using MediatR;

namespace CashFlow.Application.Commands;

/// <summary>
/// Create category command
/// </summary>
/// <param name="Name">Category name</param>
public sealed record CreateCategoryCommand(string Name) : IRequest<CategoryDto>;