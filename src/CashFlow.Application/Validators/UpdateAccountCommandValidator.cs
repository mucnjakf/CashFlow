using CashFlow.Application.Commands;
using FluentValidation;

namespace CashFlow.Application.Validators;

public sealed class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
{
    public UpdateAccountCommandValidator()
    {
        RuleFor(x => x.Balance).GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be a positive number");
    }
}