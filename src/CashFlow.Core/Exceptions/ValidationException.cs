using System.Net;

namespace CashFlow.Core.Exceptions;

public sealed class ValidationException(string message, IEnumerable<string> errors) : HttpException(HttpStatusCode.BadRequest, message)
{
    public IEnumerable<string> Errors { get; private set; } = errors;
}