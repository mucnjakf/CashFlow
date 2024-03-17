using CashFlow.Application.Dtos;
using CashFlow.Core.Entities;

namespace CashFlow.Application.Mappers;

public static class CategoryMapper
{
    public static CategoryDto ToCategoryDto(this Category category)
    {
        return new CategoryDto(category.Id, category.Name, category.Transactions?.Select(x => x.ToCategoryTransactionDto()));
    }
    
    public static TransactionCategoryDto ToTransactionCategoryDto(this Category category)
    {
        return new TransactionCategoryDto(category.Id, category.Name);
    }
}