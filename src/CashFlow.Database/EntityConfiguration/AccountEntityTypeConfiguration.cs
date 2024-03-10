using CashFlow.Core.Entities;
using CashFlow.Database.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashFlow.Database.EntityConfiguration;

internal sealed class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
{
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