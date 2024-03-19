using System.Net;

namespace CashFlow.Core.Exceptions;

public sealed class CategoryException(HttpStatusCode httpStatusCode, string message) : HttpException(httpStatusCode, message);