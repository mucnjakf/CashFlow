using System.Diagnostics;
using CashFlow.Core.Constants;
using CashFlow.Core.Entities.Abstract;
using CashFlow.Core.Enums;
using CashFlow.Core.Exceptions;

namespace CashFlow.Core.Entities;

public sealed class Account : Entity
{
    public double Balance { get; private set; }

    public IList<Transaction>? Transactions { get; private set; }

    private Account(Guid id, double balance) : base(id)
    {
        Balance = balance;
    }

    public static Account Create(double balance = 0)
    {
        if (balance < 0)
        {
            throw new AccountException(Errors.Account.BalancePositiveNumber);
        }

        Guid id = Guid.NewGuid();

        return new Account(id, balance);
    }

    public void Update(double balance = 0)
    {
        if (balance < 0)
        {
            throw new AccountException(Errors.Account.BalancePositiveNumber);
        }

        Balance = balance;
        UpdatedUtc = DateTime.UtcNow;
    }

    public void UpdateBalance(TransactionType type, double amount)
    {
        if (Balance < amount)
        {
            throw new AccountException(Errors.Account.InsufficientBalance);
        }

        if (amount <= 0)
        {
            throw new TransactionException(Errors.Transaction.AmountGreaterThanZero);
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

    public void RevertBalance(TransactionType type, double amount)
    {
        if (Balance < amount)
        {
            throw new AccountException(Errors.Account.InsufficientBalance);
        }

        if (amount <= 0)
        {
            throw new TransactionException(Errors.Transaction.AmountGreaterThanZero);
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