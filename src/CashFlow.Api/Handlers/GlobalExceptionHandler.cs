using System.Net;
using System.Net.Mime;
using System.Text.Json.Serialization;
using CashFlow.Core.Constants;
using CashFlow.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace CashFlow.Api.Handlers;

/// <summary>
/// Global exception handler
/// </summary>
/// <param name="logger"><see cref="ILogger{TCategoryName}"/></param>
internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    /// <summary>
    /// Exception handler
    /// </summary>
    /// <param name="httpContext"><see cref="HttpContext"/></param>
    /// <param name="exception"><see cref="Exception"/> thrown in the application</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns></returns>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        ErrorResponseDto errorResponse = GetErrorResponse(exception);

        logger.LogError("Http Status Code {httpStatusCode} - Error message: {Message}",
            errorResponse.HttpStatusCode, errorResponse.Message);

        httpContext.Response.ContentType = MediaTypeNames.Application.Json;
        httpContext.Response.StatusCode = (int)errorResponse.HttpStatusCode;

        await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);

        return true;
    }

    /// <summary>
    /// Converts exception into error response DTO
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    private static ErrorResponseDto GetErrorResponse(Exception exception) => exception switch
    {
        AccountException aex => new ErrorResponseDto(aex.HttpStatusCode, aex.Message),

        TransactionException tex => new ErrorResponseDto(tex.HttpStatusCode, tex.Message),

        CategoryException cex => new ErrorResponseDto(cex.HttpStatusCode, cex.Message),

        ValidationException vex => new ErrorResponseDto(vex.HttpStatusCode, vex.Message, vex.Errors),

        _ => new ErrorResponseDto(HttpStatusCode.InternalServerError, Errors.General.UnhandledError)
    };

    /// <summary>
    /// Error response DTO
    /// </summary>
    /// <param name="HttpStatusCode">HTTP status code that will be returned</param>
    /// <param name="Message">Error message</param>
    /// <param name="ValidationErrors">Validation error messages</param>
    public sealed record ErrorResponseDto(
        [property: JsonIgnore] HttpStatusCode HttpStatusCode,
        string Message,
        IEnumerable<string>? ValidationErrors = null);
}