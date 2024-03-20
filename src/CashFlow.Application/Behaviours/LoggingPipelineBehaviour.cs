using MediatR;
using Microsoft.Extensions.Logging;

namespace CashFlow.Application.Behaviours;

/// <summary>
/// Logging pipeline behaviour
/// </summary>
/// <param name="logger"><see cref="ILogger{TCategoryName}"/></param>
/// <typeparam name="TRequest">Request coming into pipeline</typeparam>
/// <typeparam name="TResponse">Response leaving pipeline</typeparam>
public sealed class LoggingPipelineBehaviour<TRequest, TResponse>(ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    /// <summary>
    /// Handles request and response logging
    /// </summary>
    /// <param name="request">Request coming into pipeline</param>
    /// <param name="next"><see cref="RequestHandlerDelegate{TResponse}"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Response leaving pipeline</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling: {RequestName}Handler", typeof(TRequest).Name);

        TResponse response = await next();

        logger.LogInformation("Handled: {RequestName}Handler", typeof(TRequest).Name);

        return response;
    }
}