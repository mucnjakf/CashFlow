using CashFlow.Core.Constants;
using CashFlow.Core.Entities;
using CashFlow.Core.Enums;
using CashFlow.Core.Exceptions;
using CashFlow.Tests.Data.Entities;
using FluentAssertions;

namespace CashFlow.Core.Tests.Entities;

public sealed class TransactionTests
{
    [Fact]
    public void Create_Should_ReturnTransaction_When_ArgumentsAreValid()
    {
        // Arrange
        DateTime dateTimeUtc = DateTime.UtcNow;
        const string description = "Motorcycle jacket";
        const double amount = 100;
        const TransactionType type = TransactionType.Expense;
        Guid accountId = AccountData.GetAccount().Id;
        Guid categoryId = CategoryData.GetCategory().Id;

        // Act
        Transaction transaction = Transaction.Create(dateTimeUtc, description, amount, type, accountId, categoryId);

        // Assert
        transaction.Should().NotBeNull();
        transaction.Id.Should().NotBeEmpty();
        transaction.CreatedUtc.Should().NotBeBefore(DateTime.UtcNow.AddSeconds(-5));
        transaction.UpdatedUtc.Should().NotBeBefore(DateTime.UtcNow.AddSeconds(-5));
        transaction.DateTimeUtc.Should().Be(dateTimeUtc);
        transaction.Description.Should().NotBeNullOrWhiteSpace();
        transaction.Description.Should().Be(description);
        transaction.Amount.Should().BePositive();
        transaction.Amount.Should().Be(amount);
        transaction.Type.Should().Be(type);
        transaction.AccountId.Should().NotBeEmpty();
        transaction.AccountId.Should().Be(accountId);
        transaction.CategoryId.Should().NotBeEmpty();
        transaction.CategoryId.Should().Be(categoryId);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Create_Should_ThrowTransactionException_When_DescriptionIsInvalid(string description)
    {
        // Arrange
        DateTime dateTimeUtc = DateTime.UtcNow;
        const double amount = 100;
        const TransactionType type = TransactionType.Expense;
        Guid accountId = AccountData.GetAccount().Id;
        Guid categoryId = CategoryData.GetCategory().Id;

        // Act
        Func<Transaction> result = () => Transaction.Create(dateTimeUtc, description, amount, type, accountId, categoryId);

        // Assert
        result.Should().ThrowExactly<TransactionException>().WithMessage(Errors.Transaction.DescriptionRequired);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void Create_Should_ThrowTransactionException_When_AmountIsInvalid(double amount)
    {
        // Arrange
        DateTime dateTimeUtc = DateTime.UtcNow;
        const string description = "Motorcycle jacket";
        const TransactionType type = TransactionType.Expense;
        Guid accountId = AccountData.GetAccount().Id;
        Guid categoryId = CategoryData.GetCategory().Id;

        // Act
        Func<Transaction> result = () => Transaction.Create(dateTimeUtc, description, amount, type, accountId, categoryId);

        // Assert
        result.Should().ThrowExactly<TransactionException>().WithMessage(Errors.Transaction.AmountGreaterThanZero);
    }

    [Fact]
    public void Update_Should_UpdateProperties_When_ArgumentsAreValid()
    {
        // Arrange
        Transaction transaction = TransactionData.GetTransaction();
        DateTime dateTimeUtc = DateTime.UtcNow.AddDays(1);
        const string description = "Ice cream";
        Guid categoryId = CategoryData.GetCategory().Id;

        // Act
        transaction.Update(dateTimeUtc, description, categoryId);

        // Assert
        transaction.UpdatedUtc.Should().NotBeBefore(DateTime.UtcNow.AddSeconds(-5));
        transaction.DateTimeUtc.Should().Be(dateTimeUtc);
        transaction.Description.Should().NotBeNullOrWhiteSpace();
        transaction.Description.Should().Be(description);
        transaction.CategoryId.Should().NotBeEmpty();
        transaction.CategoryId.Should().Be(categoryId);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Update_Should_ThrowTransactionException_WhenArgumentsAreInvalid(string description)
    {
        // Arrange
        Transaction transaction = TransactionData.GetTransaction();

        // Act
        Action result = () => transaction.Update(DateTime.UtcNow, description, CategoryData.GetCategory().Id);

        // Assert
        result.Should().ThrowExactly<TransactionException>().WithMessage(Errors.Transaction.DescriptionRequired);
    }
}