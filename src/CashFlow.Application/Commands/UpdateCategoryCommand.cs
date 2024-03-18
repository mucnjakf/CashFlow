using MediatR;

namespace CashFlow.Application.Commands;

public sealed record UpdateCategoryCommand(Guid Id, string Name) : IRequest;