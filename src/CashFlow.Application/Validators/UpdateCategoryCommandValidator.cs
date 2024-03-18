﻿using CashFlow.Application.Commands;
using CashFlow.Core.Constants;
using FluentValidation;

namespace CashFlow.Application.Validators;

public sealed class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(Errors.Category.NameRequired);
    }
}