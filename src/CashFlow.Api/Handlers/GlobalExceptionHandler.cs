using System.Net;
using System.Net.Mime;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics;

namespace CashFlow.Api.Handlers;

internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ErrorResponseDto errorResponse = GetErrorResponse(exception);

        httpContext.Response.ContentType = MediaTypeNames.Application.Json;
        httpContext.Response.StatusCode = errorResponse.HttpStatusCode;

        await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);

        return true;
    }

    private static ErrorResponseDto GetErrorResponse(Exception exception) => exception switch
    {
        _ => new ErrorResponseDto("Unhandled error occured", (int)HttpStatusCode.InternalServerError)
    };

    private sealed record ErrorResponseDto(string Message, [property: JsonIgnore] int HttpStatusCode);
}