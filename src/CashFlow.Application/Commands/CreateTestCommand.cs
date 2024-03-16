using CashFlow.Application.Dtos;
using MediatR;

namespace CashFlow.Application.Commands;

public sealed record CreateTestCommand(string Name) : IRequest<TestDto>;