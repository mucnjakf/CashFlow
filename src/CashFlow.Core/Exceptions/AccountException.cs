using System.Net;

namespace CashFlow.Core.Exceptions;

public sealed class AccountException(HttpStatusCode httpStatusCode, string message) : HttpException(httpStatusCode, message);