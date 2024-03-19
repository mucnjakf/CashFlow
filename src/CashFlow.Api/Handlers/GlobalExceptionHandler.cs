using System.Net;
using System.Net.Mime;
using System.Text.Json.Serialization;
using CashFlow.Core.Constants;
using CashFlow.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace CashFlow.Api.Handlers;

internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        ErrorResponseDto errorResponse = GetErrorResponse(exception);

        httpContext.Response.ContentType = MediaTypeNames.Application.Json;
        httpContext.Response.StatusCode = (int)errorResponse.HttpStatusCode;

        await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);

        return true;
    }

    private static ErrorResponseDto GetErrorResponse(Exception exception) => exception switch
    {
        AccountException aex => new ErrorResponseDto(aex.HttpStatusCode, aex.Message),

        TransactionException tex => new ErrorResponseDto(tex.HttpStatusCode, tex.Message),

        CategoryException cex => new ErrorResponseDto(cex.HttpStatusCode, cex.Message),

        ValidationException vex => new ErrorResponseDto(vex.HttpStatusCode, vex.Message, vex.Errors),

        _ => new ErrorResponseDto(HttpStatusCode.InternalServerError, Errors.General.UnhandledError)
    };

    public sealed record ErrorResponseDto(
        [property: JsonIgnore] HttpStatusCode HttpStatusCode,
        string Message,
        IEnumerable<string>? ValidationErrors = null);
}