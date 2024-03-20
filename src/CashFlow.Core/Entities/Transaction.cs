using System.Net;
using CashFlow.Core.Constants;
using CashFlow.Core.Entities.Abstract;
using CashFlow.Core.Enums;
using CashFlow.Core.Exceptions;

namespace CashFlow.Core.Entities;

/// <summary>
/// Transaction entity
/// </summary>
public sealed class Transaction : Entity
{
    public DateTime DateTimeUtc { get; private set; }

    public string Description { get; private set; }

    public double Amount { get; private set; }

    public TransactionType Type { get; private set; }

    public Guid AccountId { get; private set; }

    public Account? Account { get; private set; }

    public Guid CategoryId { get; private set; }

    public Category? Category { get; private set; }

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

    /// <summary>
    /// Creates transaction entity
    /// </summary>
    /// <param name="dateTimeUtc">Date and time of a transaction</param>
    /// <param name="description">Transaction description</param>
    /// <param name="amount">Transaction amount</param>
    /// <param name="type"><see cref="TransactionType"/></param>
    /// <param name="accountId">Transaction account</param>
    /// <param name="categoryId">Transaction category</param>
    /// <returns><see cref="Transaction"/></returns>
    /// <exception cref="TransactionException">Description is required</exception>
    /// <exception cref="TransactionException">Amount must be greater than 0</exception>
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
            throw new TransactionException(HttpStatusCode.BadRequest, Errors.Transaction.DescriptionRequired);
        }

        if (amount <= 0)
        {
            throw new TransactionException(HttpStatusCode.BadRequest, Errors.Transaction.AmountGreaterThanZero);
        }

        Guid id = Guid.NewGuid();

        return new Transaction(id, dateTimeUtc, description, amount, type, accountId, categoryId);
    }

    /// <summary>
    /// Updates transaction entity
    /// </summary>
    /// <param name="dateTimeUtc">Date and time of a transaction</param>
    /// <param name="description">Transaction description</param>
    /// <param name="categoryId">Transaction category</param>
    /// <exception cref="TransactionException">Description is required</exception>
    public void Update(DateTime dateTimeUtc, string description, Guid categoryId)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new TransactionException(HttpStatusCode.BadRequest, Errors.Transaction.DescriptionRequired);
        }

        DateTimeUtc = dateTimeUtc;
        Description = description;
        CategoryId = categoryId;
        UpdatedUtc = DateTime.UtcNow;
    }
}