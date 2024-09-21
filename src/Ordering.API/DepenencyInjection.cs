using BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Ordering.API;

internal static class DepenencyInjection
{
    internal static IServiceCollection AddApiServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddCarter();
        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddHealthChecks().AddSqlServer(configuration.GetConnectionString("Database")!);

        return services;
    }

    public static WebApplication UseApiService(this WebApplication application)
    {
        application.MapGroup("/api/v1").MapCarter();
        application.UseExceptionHandler(_ => { });
        application.UseHealthChecks(
            "/api/v1/health",
            new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse }
        );
        return application;
    }
}
