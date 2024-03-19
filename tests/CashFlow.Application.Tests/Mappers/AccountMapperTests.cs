using CashFlow.Application.Dtos;
using CashFlow.Application.Mappers;
using CashFlow.Core.Entities;
using CashFlow.Tests.Data.Entities;
using FluentAssertions;

namespace CashFlow.Application.Tests.Mappers;

public sealed class AccountMapperTests
{
    [Fact]
    public void ToAccountDto_Should_ReturnAccountDto_When_AccountIsValid()
    {
        // Arrange
        Account account = AccountData.GetAccount();

        // Act
        AccountDto dto = account.ToAccountDto();

        // Assert
        dto.Should().NotBeNull();
        dto.Id.Should().Be(account.Id);
        dto.Balance.Should().Be(account.Balance);
        dto.TransactionsCount.Should().Be(account.Transactions?.Count ?? 0);
    }

    [Fact]
    public void ToTransactionAccountDto_Should_ReturnTransactionAccountDto_When_AccountIsValid()
    {
        // Arrange
        Account account = AccountData.GetAccount();

        // Act
        TransactionAccountDto dto = account.ToTransactionAccountDto();

        // Assert
        dto.Should().NotBeNull();
        dto.Id.Should().Be(account.Id);
        dto.Balance.Should().Be(account.Balance);
    }
}