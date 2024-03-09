using System.Reflection;
using CashFlow.Application.Behaviours;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application;

public static class Bootstrapper
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        Assembly assembly = typeof(Bootstrapper).Assembly;

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
            configuration.AddOpenBehavior(typeof(ValidationPipelineBehaviour<,>));
        });

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}