using CashFlow.Core.Constants;
using CashFlow.Core.Entities.Abstract;
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
}