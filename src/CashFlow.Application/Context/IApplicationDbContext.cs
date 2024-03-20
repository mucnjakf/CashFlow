using CashFlow.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Application.Context;

/// <summary>
/// Application database context
/// </summary>
public interface IApplicationDbContext
{
    DbSet<Account> Accounts { get; set; }

    DbSet<Transaction> Transactions { get; set; }

    DbSet<Category> Categories { get; set; }

    /// <summary>
    /// Save changes to database
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Task result containing the number of state entries written to the database</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}