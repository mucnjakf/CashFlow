using CashFlow.Application.Commands;
using CashFlow.Application.Context;
using CashFlow.Application.Dtos;
using CashFlow.Application.Mappers;
using CashFlow.Core.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CashFlow.Application.CommandHandlers;

internal sealed class CreateCategoryCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    public async Task<CategoryDto> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        Category category = Category.Create(command.Name);

        await dbContext.Categories.AddAsync(category, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return category.ToCategoryDto();
    }
}