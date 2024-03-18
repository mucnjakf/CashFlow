using CashFlow.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Application.Context;

public interface IApplicationDbContext
{
    DbSet<Account> Accounts { get; set; }

    DbSet<Transaction> Transactions { get; set; }

    DbSet<Category> Categories { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}