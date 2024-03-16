using CashFlow.Core.Constants;
using CashFlow.Core.Entities;
using CashFlow.Core.Exceptions;
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
        result.Should().ThrowExactly<AccountException>().WithMessage(Errors.Account.BalanceGreaterThanZero);
    }
}