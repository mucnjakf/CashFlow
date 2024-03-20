using System.Reflection;
using CashFlow.Application.Behaviours;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application;

/// <summary>
/// Bootstraps the application
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Configures application
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        Assembly assembly = typeof(Bootstrapper).Assembly;

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
            configuration.AddOpenBehavior(typeof(ValidationPipelineBehaviour<,>));
            configuration.AddOpenBehavior(typeof(LoggingPipelineBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}