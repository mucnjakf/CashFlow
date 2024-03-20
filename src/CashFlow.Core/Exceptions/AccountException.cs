﻿using System.Net;

namespace CashFlow.Core.Exceptions;

/// <summary>
/// Account exception
/// </summary>
/// <param name="httpStatusCode">HTTP status code that will be returned from API</param>
/// <param name="message">Error message</param>
public sealed class AccountException(HttpStatusCode httpStatusCode, string message) : HttpException(httpStatusCode, message);