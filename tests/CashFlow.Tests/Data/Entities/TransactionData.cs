using CashFlow.Core.Entities;
using CashFlow.Core.Enums;

namespace CashFlow.Tests.Data.Entities;

public static class TransactionData
{
    public static Transaction GetTransaction()
    {
        return Transaction.Create(
            DateTime.UtcNow,
            "Motorcycle jacket",
            100,
            TransactionType.Expense,
            AccountData.GetAccount().Id,
            CategoryData.GetCategory().Id);
    }
}