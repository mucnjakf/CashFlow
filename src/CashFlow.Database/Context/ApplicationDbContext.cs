using CashFlow.Application.Context;
using CashFlow.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Database.Context;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext 
{
    public DbSet<Account> Accounts { get; set; } = default!;

    public DbSet<Transaction> Transactions { get; set; } = default!;

    public DbSet<Category> Categories { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}