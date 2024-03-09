using CashFlow.Application;

namespace CashFlow.Api;

public static class Bootstrapper
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

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

        app.UseHttpsRedirection();

        return app;
    }
}