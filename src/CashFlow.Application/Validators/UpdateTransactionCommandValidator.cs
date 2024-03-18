using CashFlow.Application.Commands;
using FluentValidation;

namespace CashFlow.Application.Validators;

public sealed class UpdateTransactionCommandValidator : AbstractValidator<UpdateTransactionCommand>
{
    public UpdateTransactionCommandValidator()
    {
        RuleFor(x => x.Description).NotEmpty().WithMessage("{PropertyName} is required");
    }
}