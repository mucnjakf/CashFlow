using CashFlow.Application.Dtos;
using CashFlow.Application.Mappers;
using CashFlow.Core.Entities;
using CashFlow.Tests.Data.Entities;
using FluentAssertions;

namespace CashFlow.Application.Tests.Mappers;

public sealed class TransactionMapperTests
{
    [Fact]
    public void ToTransactionDto_Should_ReturnTransactionDto_When_TransactionIsValid()
    {
        // Arrange
        Transaction transaction = TransactionData.GetTransaction();

        // Act
        TransactionDto dto = transaction.ToTransactionDto();

        // Assert
        dto.Should().NotBeNull();
        dto.Id.Should().Be(transaction.Id);
        dto.DateTimeUtc.Should().Be(transaction.DateTimeUtc);
        dto.Description.Should().Be(transaction.Description);
        dto.Amount.Should().Be(transaction.Amount);
        dto.Type.Should().Be(transaction.Type);
    }
}