namespace CashFlow.Core.Exceptions;

public sealed class ValidationException(string message, IEnumerable<string> errors) : Exception(message)
{
    public IEnumerable<string> Errors { get; private set; } = errors;
}