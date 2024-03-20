using MediatR;

namespace CashFlow.Application.Commands;

/// <summary>
/// Delete category command
/// </summary>
/// <param name="Id">Category ID</param>
public sealed record DeleteCategoryCommand(Guid Id) : IRequest;