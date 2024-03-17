using MediatR;

namespace CashFlow.Application.Commands;

public sealed record DeleteCategoryCommand(Guid Id) : IRequest;