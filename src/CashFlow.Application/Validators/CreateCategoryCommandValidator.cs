using CashFlow.Application.Commands;
using CashFlow.Core.Constants;
using FluentValidation;

namespace CashFlow.Application.Validators;

/// <summary>
/// Create category command validator
/// </summary>
public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(Errors.Category.NameRequired);
    }
}