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
/// Update transaction command handler
/// </summary>
/// <param name="dbContext"><see cref="IApplicationDbContext"/></param>
internal sealed class UpdateTransactionCommandHandler(IApplicationDbContext dbContext) : IRequestHandler<UpdateTransactionCommand>
{
    /// <summary>
    /// Handles updating transaction
    /// </summary>
    /// <param name="command"><see cref="UpdateTransactionCommand"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <exception cref="TransactionException">Transaction not found</exception>
    /// <exception cref="CategoryException">Category not found</exception>
    public async Task Handle(UpdateTransactionCommand command, CancellationToken cancellationToken)
    {
        Transaction? transaction = await dbContext.Transactions
            .SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

        if (transaction is null)
        {
            throw new TransactionException(HttpStatusCode.NotFound, Errors.Transaction.TransactionNotFound);
        }

        Category? category = await dbContext.Categories
            .SingleOrDefaultAsync(x => x.Id == command.CategoryId, cancellationToken);

        if (category is null)
        {
            throw new CategoryException(HttpStatusCode.NotFound, Errors.Category.CategoryNotFound);
        }

        transaction.Update(command.DateTimeUtc, command.Description, category.Id);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}