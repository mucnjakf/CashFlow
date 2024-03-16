using CashFlow.Application.Dtos;
using CashFlow.Application.Mappers;
using CashFlow.Application.Queries;
using CashFlow.Core.Entities;
using CashFlow.Database.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Application.Handlers;

internal sealed class GetTransactionsQueryHandler(ApplicationDbContext dbContext) : IRequestHandler<GetTransactionsQuery, IEnumerable<TransactionDto>>
{
    public async Task<IEnumerable<TransactionDto>> Handle(GetTransactionsQuery query, CancellationToken cancellationToken)
    {
        List<Transaction> transactions = await dbContext.Transactions
            .Include(x => x.Account)
            .Include(x => x.Category)
            .ToListAsync(cancellationToken);

        return transactions.Select(x => x.ToTransactionDto());
    }
}