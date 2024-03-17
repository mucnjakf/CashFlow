using CashFlow.Application.Commands;
using CashFlow.Core.Constants;
using CashFlow.Core.Entities;
using CashFlow.Core.Exceptions;
using CashFlow.Database.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Application.CommandHandlers;

internal sealed class UpdateCategoryCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<UpdateCategoryCommand>
{
    public async Task Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        Category? category = await dbContext.Categories.SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (category is null)
        {
            throw new CategoryException(Errors.Category.CategoryNotFound);
        }

        category.Update(command.Name);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}