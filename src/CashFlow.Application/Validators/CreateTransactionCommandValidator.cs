using CashFlow.Application.Commands;
using CashFlow.Core.Constants;
using FluentValidation;

namespace CashFlow.Application.Validators;

/// <summary>
/// Create transaction command validator
/// </summary>
public sealed class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
{
    public CreateTransactionCommandValidator()
    {
        RuleFor(x => x.Description).NotEmpty().WithMessage(Errors.Transaction.DescriptionRequired);

        RuleFor(x => x.Amount).GreaterThan(0).WithMessage(Errors.Transaction.AmountGreaterThanZero);

        RuleFor(x => x.Type).IsInEnum().WithMessage(Errors.Transaction.TypeRequired);
    }
}