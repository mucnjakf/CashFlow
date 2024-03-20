using CashFlow.Core.Entities;
using CashFlow.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashFlow.Database.EntityConfiguration;

/// <summary>
/// Account entity type configuration
/// </summary>
internal sealed class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
{
    /// <summary>
    /// Configures account entity type
    /// </summary>
    /// <param name="builder"><see cref="EntityTypeBuilder{TEntity}"/></param>
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable(TableNames.Account);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedUtc).IsRequired();

        builder.Property(x => x.UpdatedUtc).IsRequired();

        builder.Property(x => x.Balance).IsRequired();

        builder
            .HasMany(x => x.Transactions)
            .WithOne(x => x.Account)
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}