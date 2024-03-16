using CashFlow.Application.Commands;
using CashFlow.Core.Constants;
using CashFlow.Core.Entities;
using CashFlow.Core.Exceptions;
using CashFlow.Database.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Application.Handlers;

internal sealed class UpdateTransactionCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<UpdateTransactionCommand>
{
    public async Task Handle(UpdateTransactionCommand command, CancellationToken cancellationToken)
    {
        Transaction? transaction = await dbContext.Transactions
            .SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (transaction is null)
        {
            throw new TransactionException(Errors.Transaction.TransactionNotFound);
        }

        Category? category = await dbContext.Categories
            .SingleOrDefaultAsync(x => x.Id == command.CategoryId, cancellationToken);

        if (category is null)
        {
            throw new CategoryException(Errors.Category.CategoryNotFound);
        }

        transaction.Update(command.DateTimeUtc, command.Description, category.Id);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}