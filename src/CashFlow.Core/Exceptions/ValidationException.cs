using System.Net;

namespace CashFlow.Core.Exceptions;

/// <summary>
/// Validation exception
/// </summary>
/// <param name="message">Error message</param>
/// <param name="errors">Validation error messages</param>
public sealed class ValidationException(string message, IEnumerable<string> errors) : HttpException(HttpStatusCode.BadRequest, message)
{
    public IEnumerable<string> Errors { get; private set; } = errors;
}