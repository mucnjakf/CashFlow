using System.Reflection;
using Carter;
using CashFlow.Api.Constants;
using CashFlow.Api.Handlers;
using CashFlow.Application;
using CashFlow.Database;
using CashFlow.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace CashFlow.Api;

public static class Bootstrapper
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(ApiVersion.V1, new OpenApiInfo
            {
                Version = ApiVersion.V1,
                Title = "CashFlow API"
            });

            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        });

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

        app.ApplyDatabaseMigrations();

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

    private static void ApplyDatabaseMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using ApplicationDbContext dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (dbContext.Database.IsRelational())
        {
            dbContext.Database.Migrate();
        }
    }
}