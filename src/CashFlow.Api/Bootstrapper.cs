using Carter;
using CashFlow.Api.Handlers;
using CashFlow.Application;

namespace CashFlow.Api;

public static class Bootstrapper
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddGlobalExceptionHandler();

        services.AddCarter();

        services.AddApplication();

        return services;
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseExceptionHandler();

        app.UseHttpsRedirection();

        app.MapCarter();

        return app;
    }

    private static IServiceCollection AddGlobalExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }
}