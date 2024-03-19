using System.Net;

namespace CashFlow.Core.Exceptions;

public sealed class TransactionException(HttpStatusCode httpStatusCode, string message) : HttpException(httpStatusCode, message);