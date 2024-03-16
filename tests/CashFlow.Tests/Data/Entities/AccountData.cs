using CashFlow.Core.Entities;

namespace CashFlow.Tests.Data.Entities;

public static class AccountData
{
    public static Account GetAccount()
    {
        return Account.Create(100);
    }
}