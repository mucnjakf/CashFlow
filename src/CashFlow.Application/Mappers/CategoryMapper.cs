using CashFlow.Application.Dtos;
using CashFlow.Core.Entities;

namespace CashFlow.Application.Mappers;

/// <summary>
/// Category mapper
/// </summary>
public static class CategoryMapper
{
    /// <summary>
    /// Maps category entity to category DTO
    /// </summary>
    /// <param name="category"><see cref="Category"/></param>
    /// <returns><see cref="CategoryDto"/></returns>
    public static CategoryDto ToCategoryDto(this Category category)
    {
        return new CategoryDto(category.Id, category.Name, category.Transactions?.Count ?? 0);
    }

    /// <summary>
    /// Maps category entity to transaction category DTO
    /// </summary>
    /// <param name="category"><see cref="Category"/></param>
    /// <returns><see cref="TransactionCategoryDto"/></returns>
    public static TransactionCategoryDto ToTransactionCategoryDto(this Category category)
    {
        return new TransactionCategoryDto(category.Id, category.Name);
    }
}