using CashFlow.Application.Commands;
using FluentValidation;

namespace CashFlow.Application.Validators;

public sealed class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} is required");
    }
}