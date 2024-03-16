using CashFlow.Application.Commands;
using CashFlow.Application.Dtos;
using MediatR;

namespace CashFlow.Application.Handlers;

internal sealed class CreateTestCommandHandler : IRequestHandler<CreateTestCommand, TestDto>
{
    public async Task<TestDto> Handle(CreateTestCommand command, CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);

        return new TestDto(command.Name);
    }
}