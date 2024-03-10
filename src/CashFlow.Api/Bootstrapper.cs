using Carter;
using CashFlow.Api.Handlers;
using CashFlow.Application;
using CashFlow.Database;

namespace CashFlow.Api;

public static class Bootstrapper
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddGlobalExceptionHandler();

        services.AddCarter();

        services
            .AddApplication()
            .AddDatabase(configuration);

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