using CashFlow.Application.Commands;
using FluentValidation;

namespace CashFlow.Application.Validators;

public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} is required");
    }
}