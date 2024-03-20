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
/// Delete category command handler
/// </summary>
/// <param name="dbContext"><see cref="IApplicationDbContext"/></param>
internal sealed class DeleteCategoryCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<DeleteCategoryCommand>
{
    /// <summary>
    /// Handles deleting category
    /// </summary>
    /// <param name="command"><see cref="DeleteCategoryCommand"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <exception cref="CategoryException">Category not found</exception>
    /// <exception cref="CategoryException">Category contains transactions</exception>
    public async Task Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        Category? category = await dbContext.Categories
            .Include(x => x.Transactions)
            .SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (category is null)
        {
            throw new CategoryException(HttpStatusCode.NotFound, Errors.Category.CategoryNotFound);
        }

        if (category.Transactions is not null && category.Transactions.Any())
        {
            throw new CategoryException(HttpStatusCode.BadRequest, Errors.Category.CategoryContainsTransactions);
        }

        dbContext.Categories.Remove(category);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}