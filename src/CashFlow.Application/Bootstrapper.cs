using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application;

public static class Bootstrapper
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}