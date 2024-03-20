using MediatR;
using Microsoft.Extensions.Logging;

namespace CashFlow.Application.Behaviours;

public sealed class LoggingPipelineBehaviour<TRequest, TResponse>(ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling: {RequestName}Handler", typeof(TRequest).Name);

        TResponse response = await next();

        logger.LogInformation("Handled: {RequestName}Handler", typeof(TRequest).Name);

        return response;
    }
}