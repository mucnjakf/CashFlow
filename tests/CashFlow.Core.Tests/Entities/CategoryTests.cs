using CashFlow.Core.Constants;
using CashFlow.Core.Entities;
using CashFlow.Core.Exceptions;
using CashFlow.Tests.Data.Entities;
using FluentAssertions;

namespace CashFlow.Core.Tests.Entities;

public sealed class CategoryTests
{
    [Fact]
    public void Create_Should_ReturnCategory_When_ArgumentsAreValid()
    {
        // Arrange
        const string name = "Shopping";

        // Act
        Category category = Category.Create(name);

        // Assert
        category.Should().NotBeNull();
        category.Id.Should().NotBeEmpty();
        category.CreatedUtc.Should().NotBeBefore(DateTime.UtcNow.AddSeconds(-5));
        category.UpdatedUtc.Should().NotBeBefore(DateTime.UtcNow.AddSeconds(-5));
        category.Name.Should().NotBeNullOrWhiteSpace();
        category.Name.Should().Be(name);
        category.Transactions.Should().BeNull();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_Should_ThrowCategoryException_When_ArgumentsAreInvalid(string name)
    {
        // Act
        Func<Category> result = () => Category.Create(name);

        // Assert
        result.Should().ThrowExactly<CategoryException>().WithMessage(Errors.Category.NameRequired);
    }
    
    [Fact]
    public void Update_Should_UpdateProperties_When_ArgumentsAreValid()
    {
        // Arrange
        Category category = CategoryData.GetCategory();
        const string name = "Motorcycle";

        // Act
        category.Update(name);
        

        // Assert
        category.UpdatedUtc.Should().NotBeBefore(DateTime.UtcNow.AddSeconds(-5));
        category.Name.Should().NotBeNullOrWhiteSpace();
        category.Name.Should().Be(name);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Update_Should_ThrowCategoryException_When_ArgumentsAreInvalid(string name)
    {
        // Arrange
        Category category = CategoryData.GetCategory();
        
        // Act
        Action result = () => category.Update(name);

        // Assert
        result.Should().ThrowExactly<CategoryException>().WithMessage(Errors.Category.NameRequired);
    }
}