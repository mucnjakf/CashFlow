using System.Net;
using CashFlow.Application.Commands;
using CashFlow.Application.Context;
using CashFlow.Core.Constants;
using CashFlow.Core.Entities;
using CashFlow.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Application.CommandHandlers;

internal sealed class DeleteCategoryCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<DeleteCategoryCommand>
{
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