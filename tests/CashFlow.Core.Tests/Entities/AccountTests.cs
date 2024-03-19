using CashFlow.Core.Constants;
using CashFlow.Core.Entities;
using CashFlow.Core.Enums;
using CashFlow.Core.Exceptions;
using CashFlow.Tests.Data.Entities;
using FluentAssertions;

namespace CashFlow.Core.Tests.Entities;

public sealed class AccountTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void Create_Should_ReturnAccount_When_ArgumentsAreValid(int balance)
    {
        // Act
        Account account = Account.Create(balance);

        // Assert
        account.Should().NotBeNull();
        account.Id.Should().NotBeEmpty();
        account.CreatedUtc.Should().NotBeBefore(DateTime.UtcNow.AddSeconds(-5));
        account.UpdatedUtc.Should().NotBeBefore(DateTime.UtcNow.AddSeconds(-5));
        account.Balance.Should().BeGreaterOrEqualTo(0);
        account.Balance.Should().Be(balance);
        account.Transactions.Should().BeNull();
    }

    [Fact]
    public void Create_Should_ThrowAccountException_When_ArgumentsAreInvalid()
    {
        // Arrange
        const int balance = -1;

        // Act
        Func<Account> result = () => Account.Create(balance);

        // Assert
        result.Should().ThrowExactly<AccountException>().WithMessage(Errors.Account.BalancePositiveNumber);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void Update_Should_UpdateProperties_When_ArgumentsAreValid(int balance)
    {
        // Arrange
        Account account = AccountData.GetAccount();

        // Act
        account.Update(balance);

        // Assert
        account.UpdatedUtc.Should().NotBeBefore(DateTime.UtcNow.AddSeconds(-5));
        account.Balance.Should().BeGreaterOrEqualTo(0);
        account.Balance.Should().Be(balance);
    }

    [Fact]
    public void Update_Should_ThrowAccountException_When_ArgumentsAreInvalid()
    {
        // Arrange
        Account account = AccountData.GetAccount();

        // Act
        Action result = () => account.Update(-1);

        // Assert
        result.Should().ThrowExactly<AccountException>().WithMessage(Errors.Account.BalancePositiveNumber);
    }

    [Fact]
    public void UpdateBalance_Should_UpdateBalance_When_TypeIncomeAndArgumentsAreValid()
    {
        // Arrange
        Account account = AccountData.GetAccount();
        const int amount = 10;
        double expected = account.Balance + amount;

        // Act
        account.UpdateBalance(TransactionType.Income, amount);

        // Assert
        account.UpdatedUtc.Should().NotBeBefore(DateTime.UtcNow.AddSeconds(-5));
        account.Balance.Should().BeGreaterThanOrEqualTo(0);
        account.Balance.Should().Be(expected);
    }

    [Fact]
    public void UpdateBalance_Should_UpdateBalance_When_TypeExpenseAndArgumentsAreValid()
    {
        // Arrange
        Account account = AccountData.GetAccount();
        const int amount = 10;
        double expected = account.Balance - amount;

        // Act
        account.UpdateBalance(TransactionType.Expense, 10);

        // Assert
        account.UpdatedUtc.Should().NotBeBefore(DateTime.UtcNow.AddSeconds(-5));
        account.Balance.Should().BeGreaterThanOrEqualTo(0);
        account.Balance.Should().Be(expected);
    }

    [Fact]
    public void UpdateBalance_Should_ThrowAccountException_When_BalanceIsLessThanAmount()
    {
        // Arrange
        Account account = AccountData.GetAccount();

        // Act
        Action result = () => account.UpdateBalance(TransactionType.Income, 200);

        // Assert
        result.Should().ThrowExactly<AccountException>().WithMessage(Errors.Account.InsufficientBalance);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void UpdateBalance_Should_ThrowTransactionException_When_AmountIsLessThanZero(double amount)
    {
        // Arrange
        Account account = AccountData.GetAccount();

        // Act
        Action result = () => account.UpdateBalance(TransactionType.Income, amount);

        // Assert
        result.Should().ThrowExactly<TransactionException>().WithMessage(Errors.Transaction.AmountGreaterThanZero);
    }
    
    [Fact]
    public void RevertBalance_Should_RevertBalance_When_TypeIncomeAndArgumentsAreValid()
    {
        // Arrange
        Account account = AccountData.GetAccount();
        const int amount = 10;
        double expected = account.Balance - amount;

        // Act
        account.RevertBalance(TransactionType.Income, amount);

        // Assert
        account.UpdatedUtc.Should().NotBeBefore(DateTime.UtcNow.AddSeconds(-5));
        account.Balance.Should().BeGreaterThanOrEqualTo(0);
        account.Balance.Should().Be(expected);
    }

    [Fact]
    public void RevertBalance_Should_RevertBalance_When_TypeExpenseAndArgumentsAreValid()
    {
        // Arrange
        Account account = AccountData.GetAccount();
        const int amount = 10;
        double expected = account.Balance + amount;

        // Act
        account.RevertBalance(TransactionType.Expense, 10);

        // Assert
        account.UpdatedUtc.Should().NotBeBefore(DateTime.UtcNow.AddSeconds(-5));
        account.Balance.Should().BeGreaterThanOrEqualTo(0);
        account.Balance.Should().Be(expected);
    }

    [Fact]
    public void RevertBalance_Should_ThrowAccountException_When_BalanceIsLessThanAmount()
    {
        // Arrange
        Account account = AccountData.GetAccount();

        // Act
        Action result = () => account.RevertBalance(TransactionType.Income, 200);

        // Assert
        result.Should().ThrowExactly<AccountException>().WithMessage(Errors.Account.InsufficientBalance);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void RevertBalance_Should_ThrowTransactionException_When_AmountIsLessThanZero(double amount)
    {
        // Arrange
        Account account = AccountData.GetAccount();

        // Act
        Action result = () => account.RevertBalance(TransactionType.Income, amount);

        // Assert
        result.Should().ThrowExactly<TransactionException>().WithMessage(Errors.Transaction.AmountGreaterThanZero);
    }
}