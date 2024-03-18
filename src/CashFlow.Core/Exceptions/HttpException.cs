using System.Net;

namespace CashFlow.Core.Exceptions;

public class HttpException(HttpStatusCode httpStatusCode, string message) : Exception(message)
{
    public HttpStatusCode HttpStatusCode { get; private set; } = httpStatusCode;
}