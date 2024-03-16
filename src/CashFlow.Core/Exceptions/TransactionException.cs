namespace CashFlow.Core.Exceptions;

public sealed class TransactionException(string message) : Exception(message);