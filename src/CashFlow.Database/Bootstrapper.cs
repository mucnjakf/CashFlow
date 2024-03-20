using CashFlow.Application.Context;
using CashFlow.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Database;

/// <summary>
/// Bootstraps the database
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Configures database
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="configuration"><see cref="IConfiguration"/></param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        string connectionString = configuration.GetConnectionString("Default")!;

        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }
}