using System.Diagnostics;
using System.Net;
using CashFlow.Core.Constants;
using CashFlow.Core.Entities.Abstract;
using CashFlow.Core.Enums;
using CashFlow.Core.Exceptions;

namespace CashFlow.Core.Entities;

/// <summary>
/// Account entity
/// </summary>
public sealed class Account : Entity
{
    public double Balance { get; private set; }

    public IList<Transaction>? Transactions { get; private set; }

    private Account(Guid id, double balance) : base(id)
    {
        Balance = balance;
    }

    /// <summary>
    /// Creates account entity
    /// </summary>
    /// <param name="balance">Account balance</param>
    /// <returns><see cref="Account"/></returns>
    /// <exception cref="AccountException">Balance must be a positive number</exception>
    public static Account Create(double balance = 0)
    {
        if (balance < 0)
        {
            throw new AccountException(HttpStatusCode.BadRequest, Errors.Account.BalancePositiveNumber);
        }

        Guid id = Guid.NewGuid();

        return new Account(id, balance);
    }

    /// <summary>
    /// Updates account entity
    /// </summary>
    /// <param name="balance">Account balance</param>
    /// <exception cref="AccountException">Balance must be a positive number</exception>
    public void Update(double balance = 0)
    {
        if (balance < 0)
        {
            throw new AccountException(HttpStatusCode.BadRequest, Errors.Account.BalancePositiveNumber);
        }

        Balance = balance;
        UpdatedUtc = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates account balance
    /// </summary>
    /// <param name="type"><see cref="TransactionType"/></param>
    /// <param name="amount">Amount to add or subtract from balance</param>
    /// <exception cref="AccountException">Insufficient balance</exception>
    /// <exception cref="TransactionException">Amount must be greater than 0</exception>
    public void UpdateBalance(TransactionType type, double amount)
    {
        if (Balance < amount)
        {
            throw new AccountException(HttpStatusCode.BadRequest, Errors.Account.InsufficientBalance);
        }

        if (amount <= 0)
        {
            throw new TransactionException(HttpStatusCode.BadRequest, Errors.Transaction.AmountGreaterThanZero);
        }

        switch (type)
        {
            case TransactionType.Income:
                Balance += amount;
                break;
            case TransactionType.Expense:
                Balance -= amount;
                break;
            default:
                throw new UnreachableException();
        }

        UpdatedUtc = DateTime.UtcNow;
    }

    /// <summary>
    /// Reverts account balance
    /// </summary>
    /// <param name="type"><see cref="TransactionType"/></param>
    /// <param name="amount">Amount to add or subtract from balance</param>
    /// <exception cref="AccountException">Insufficient balance</exception>
    /// <exception cref="TransactionException">Amount must be greater than 0</exception>
    public void RevertBalance(TransactionType type, double amount)
    {
        if (Balance < amount)
        {
            throw new AccountException(HttpStatusCode.BadRequest, Errors.Account.InsufficientBalance);
        }

        if (amount <= 0)
        {
            throw new TransactionException(HttpStatusCode.BadRequest, Errors.Transaction.AmountGreaterThanZero);
        }

        switch (type)
        {
            case TransactionType.Income:
                Balance -= amount;
                break;
            case TransactionType.Expense:
                Balance += amount;
                break;
            default:
                throw new UnreachableException();
        }

        UpdatedUtc = DateTime.UtcNow;
    }
}