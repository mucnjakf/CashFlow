using CashFlow.Application.Dtos;
using CashFlow.Core.Entities;

namespace CashFlow.Application.Mappers;

public static class CategoryMapper
{
    public static TransactionCategoryDto ToTransactionCategoryDto(this Category category)
    {
        return new TransactionCategoryDto(category.Id, category.Name);
    }
}