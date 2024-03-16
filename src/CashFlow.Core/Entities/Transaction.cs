using CashFlow.Core.Constants;
using CashFlow.Core.Entities.Abstract;
using CashFlow.Core.Enums;
using CashFlow.Core.Exceptions;

namespace CashFlow.Core.Entities;

public sealed class Transaction : Entity
{
    public DateTime DateTimeUtc { get; private set; }

    public string Description { get; private set; }

    public double Amount { get; private set; }

    public TransactionType Type { get; private set; }

    public Guid AccountId { get; private set; }

    public Account Account { get; private set; } = default!;

    public Guid CategoryId { get; private set; }

    public Category Category { get; private set; } = default!;

    private Transaction(Guid id,
        DateTime dateTimeUtc,
        string description,
        double amount,
        TransactionType type,
        Guid accountId,
        Guid categoryId) : base(id)
    {
        DateTimeUtc = dateTimeUtc;
        Description = description;
        Amount = amount;
        Type = type;
        AccountId = accountId;
        CategoryId = categoryId;
    }

    public static Transaction Create(
        DateTime dateTimeUtc,
        string description,
        double amount,
        TransactionType type,
        Guid accountId,
        Guid categoryId)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new TransactionException(Errors.Transaction.DescriptionRequired);
        }

        if (amount < 0)
        {
            throw new TransactionException(Errors.Transaction.AmountGreaterThanZero);
        }

        Guid id = Guid.NewGuid();

        return new Transaction(id, dateTimeUtc, description, amount, type, accountId, categoryId);
    }

    public void Update(DateTime dateTimeUtc, string description, Guid categoryId)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new TransactionException(Errors.Transaction.DescriptionRequired);
        }

        DateTimeUtc = dateTimeUtc;
        Description = description;
        CategoryId = categoryId;
        UpdatedUtc = DateTime.UtcNow;
    }
}