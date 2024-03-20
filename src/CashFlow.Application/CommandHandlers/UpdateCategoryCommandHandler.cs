using System.Net;
using CashFlow.Application.Commands;
using CashFlow.Application.Context;
using CashFlow.Core.Constants;
using CashFlow.Core.Entities;
using CashFlow.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Application.CommandHandlers;

/// <summary>
/// Update category command handler
/// </summary>
/// <param name="dbContext"><see cref="IApplicationDbContext"/></param>
internal sealed class UpdateCategoryCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<UpdateCategoryCommand>
{
    /// <summary>
    /// Handles updating category
    /// </summary>
    /// <param name="command"><see cref="UpdateCategoryCommand"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <exception cref="CategoryException">Category not found</exception>
    public async Task Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        Category? category = await dbContext.Categories.SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (category is null)
        {
            throw new CategoryException(HttpStatusCode.NotFound, Errors.Category.CategoryNotFound);
        }

        category.Update(command.Name);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}