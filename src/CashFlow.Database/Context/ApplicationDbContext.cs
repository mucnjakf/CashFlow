using CashFlow.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Database.Context;

internal sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; private set; } = default!;

    public DbSet<Transaction> Transactions { get; private set; } = default!;

    public DbSet<Category> Categories { get; private set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}