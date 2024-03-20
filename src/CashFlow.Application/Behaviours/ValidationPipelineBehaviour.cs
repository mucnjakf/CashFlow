using FluentValidation;
using MediatR;
using ValidationException = CashFlow.Core.Exceptions.ValidationException;

namespace CashFlow.Application.Behaviours;

/// <summary>
/// Validation pipeline behaviour
/// </summary>
/// <param name="validators"><see cref="IValidator{T}"/></param>
/// <typeparam name="TRequest">Request coming into pipeline</typeparam>
/// <typeparam name="TResponse">Response leaving pipeline</typeparam>
public sealed class ValidationPipelineBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    /// <summary>
    /// Handles request validation
    /// </summary>
    /// <param name="request">Request coming into pipeline</param>
    /// <param name="next"><see cref="RequestHandlerDelegate{TResponse}"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Response leaving pipeline</returns>
    /// <exception cref="ValidationException">Request is invalid with validation errors</exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }

        List<string> errors = validators.Select(x => x.Validate(request))
            .SelectMany(x => x.Errors)
            .Select(x => x.ErrorMessage)
            .ToList();

        if (errors.Count != 0)
        {
            throw new ValidationException($"{typeof(TRequest).Name} is invalid", errors);
        }

        return await next();
    }
}