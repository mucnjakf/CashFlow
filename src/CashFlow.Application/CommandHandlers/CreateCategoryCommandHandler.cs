using CashFlow.Application.Commands;
using CashFlow.Application.Context;
using CashFlow.Application.Dtos;
using CashFlow.Application.Mappers;
using CashFlow.Core.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CashFlow.Application.CommandHandlers;

/// <summary>
/// Create category command handler
/// </summary>
/// <param name="dbContext"><see cref="IApplicationDbContext"/></param>
internal sealed class CreateCategoryCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    /// <summary>
    /// Handles creating category
    /// </summary>
    /// <param name="command"><see cref="CreateCategoryCommand"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns><see cref="CategoryDto"/></returns>
    public async Task<CategoryDto> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        Category category = Category.Create(command.Name);

        await dbContext.Categories.AddAsync(category, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return category.ToCategoryDto();
    }
}