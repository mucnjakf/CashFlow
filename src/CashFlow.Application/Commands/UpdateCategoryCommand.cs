using MediatR;

namespace CashFlow.Application.Commands;

// TODO; fluent validation
public sealed record UpdateCategoryCommand(Guid Id, string Name) : IRequest;