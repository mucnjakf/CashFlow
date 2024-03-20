using CashFlow.Core.Entities;
using CashFlow.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashFlow.Database.EntityConfiguration;

/// <summary>
/// Category entity type configuration
/// </summary>
internal sealed class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
{
    /// <summary>
    /// Configures category entity type
    /// </summary>
    /// <param name="builder"><see cref="EntityTypeBuilder{TEntity}"/></param>
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(TableNames.Category);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedUtc).IsRequired();

        builder.Property(x => x.UpdatedUtc).IsRequired();

        builder.Property(x => x.Name).IsRequired();

        builder
            .HasMany(x => x.Transactions)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}