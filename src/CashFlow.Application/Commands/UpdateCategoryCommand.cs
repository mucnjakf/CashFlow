using MediatR;

namespace CashFlow.Application.Commands;

/// <summary>
/// Update category command
/// </summary>
/// <param name="Id">Category ID</param>
/// <param name="Name">Category name</param>
public sealed record UpdateCategoryCommand(Guid Id, string Name) : IRequest;