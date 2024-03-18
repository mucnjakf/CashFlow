using CashFlow.Application.Commands;
using FluentValidation;

namespace CashFlow.Application.Validators;

public sealed class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        RuleFor(x => x.Description).NotEmpty().WithMessage("{PropertyName} is required");

        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(x => x.Type).IsInEnum().WithMessage("{PropertyName} is required");
    }
}