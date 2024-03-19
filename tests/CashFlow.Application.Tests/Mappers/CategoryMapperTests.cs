using CashFlow.Application.Dtos;
using CashFlow.Application.Mappers;
using CashFlow.Core.Entities;
using CashFlow.Tests.Data.Entities;
using FluentAssertions;

namespace CashFlow.Application.Tests.Mappers;

public sealed class CategoryMapperTests
{
    [Fact]
    public void ToCategoryDto_Should_ReturnCategoryDto_When_CategoryIsValid()
    {
        // Arrange
        Category category = CategoryData.GetCategory();

        // Act
        CategoryDto dto = category.ToCategoryDto();

        // Assert
        dto.Should().NotBeNull();
        dto.Id.Should().Be(category.Id);
        dto.Name.Should().Be(category.Name);
        dto.TransactionsCount.Should().Be(category.Transactions?.Count ?? 0);
    }

    [Fact]
    public void ToTransactionCategoryDto_Should_ReturnTransactionCategoryDto_When_CategoryIsValid()
    {
        // Arrange
        Category category = CategoryData.GetCategory();

        // Act
        TransactionCategoryDto dto = category.ToTransactionCategoryDto();

        // Assert
        dto.Should().NotBeNull();
        dto.Id.Should().Be(category.Id);
        dto.Name.Should().Be(category.Name);
    }
}