﻿using System.Reflection;
using System.Text.Json.Serialization;
using Asp.Versioning;
using Carter;
using CashFlow.Api.Handlers;
using CashFlow.Application;
using CashFlow.Database;
using CashFlow.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace CashFlow.Api;

/// <summary>
/// Bootstraps the API
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Configures dependency injection
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="configuration"><see cref="IConfiguration"/></param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO: configure cors
        services.AddCors(options => options.AddPolicy("Default",
            policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

        services.AddEndpointsApiExplorer();

        services.AddSwagger();

        services.AddGlobalExceptionHandler();

        services.AddCarter();

        services
            .AddApplication()
            .AddDatabase(configuration);

        return services;
    }

    /// <summary>
    /// Configures the middleware pipeline
    /// </summary>
    /// <param name="app"><see cref="WebApplication"/></param>
    /// <returns><see cref="WebApplication"/></returns>
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseCors("Default");

        app.UseSwagger();
        app.UseSwaggerUI();

        app.ApplyDatabaseMigrations();

        app.UseExceptionHandler();

        app.UseHttpsRedirection();

        app.MapCarter();

        return app;
    }

    /// <summary>
    /// Configures Swagger
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <returns><see cref="IServiceCollection"/></returns>
    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "CashFlow API"
            });

            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        });

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    /// <summary>
    /// Configures global exception handler
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <returns><see cref="IServiceCollection"/></returns>
    private static IServiceCollection AddGlobalExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }

    /// <summary>
    /// Applies Entity Framework Core database migrations
    /// </summary>
    /// <param name="app"><see cref="IApplicationBuilder"/></param>
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