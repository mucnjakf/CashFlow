using System.Net;
using System.Net.Mime;
using System.Text.Json.Serialization;
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
        httpContext.Response.StatusCode = errorResponse.HttpStatusCode;

        await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);

        return true;
    }

    private static ErrorResponseDto GetErrorResponse(Exception exception) => exception switch
    {
        ValidationException vex => new ErrorResponseDto((int)HttpStatusCode.BadRequest, vex.Message, vex.Errors),

        _ => new ErrorResponseDto((int)HttpStatusCode.InternalServerError, "Unhandled error occured")
    };

    private sealed record ErrorResponseDto(
        [property: JsonIgnore] int HttpStatusCode,
        string Message,
        IEnumerable<string>? ValidationErrors = null);
}