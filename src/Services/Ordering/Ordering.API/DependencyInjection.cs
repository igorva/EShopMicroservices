using BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCarter(); // REF 1
        services.AddExceptionHandler<CustomExceptionHandler>(); // REF 2
        services.AddHealthChecks() // REF 3
            .AddSqlServer(configuration.GetConnectionString("Database")!);


        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter(); // REF 1
        app.UseExceptionHandler(options => { }); // REF 2
        app.UseHealthChecks("/health", //, // REF 3
            new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });


        return app;
    }
}
