using CashFlow.Core.Entities;
using CashFlow.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashFlow.Database.EntityConfiguration;

/// <summary>
/// Transaction entity type configuration
/// </summary>
internal sealed class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<Transaction>
{
    /// <summary>
    /// Configures transaction entity type
    /// </summary>
    /// <param name="builder"><see cref="EntityTypeBuilder{TEntity}"/></param>
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable(TableNames.Transaction);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.DateTimeUtc).IsRequired();

        builder.Property(x => x.Description).IsRequired();

        builder.Property(x => x.Amount).IsRequired();

        builder.Property(x => x.Type).IsRequired();
    }
}