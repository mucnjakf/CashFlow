using CashFlow.Core.Entities;

namespace CashFlow.Tests.Data.Entities;

public static class CategoryData
{
    public static Category GetCategory()
    {
        return Category.Create("Shopping");
    }
}