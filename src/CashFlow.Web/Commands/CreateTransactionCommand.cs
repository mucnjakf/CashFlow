using CashFlow.Web.Enums;

namespace CashFlow.Web.Commands;

internal sealed record CreateTransactionCommand(
    DateTime DateTimeUtc,
    string Description,
    double Amount,
    TransactionType Type,
    Guid CategoryId);