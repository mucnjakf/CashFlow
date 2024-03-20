using System.Net;

namespace CashFlow.Core.Exceptions;

/// <summary>
/// HTTP exception
/// </summary>
/// <param name="httpStatusCode">HTTP status code that will be returned from API</param>
/// <param name="message">Error message</param>
public class HttpException(HttpStatusCode httpStatusCode, string message) : Exception(message)
{
    public HttpStatusCode HttpStatusCode { get; private set; } = httpStatusCode;
}