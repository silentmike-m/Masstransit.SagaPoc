namespace Masstransit.SagaPoc.Publisher.Infrastructure.Swagger;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

internal static class DependencyInjection
{
    public static IServiceCollection AddPoCSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureSwaggerGen(c =>
        {
            c.CustomSchemaIds(s => s.FullName);
        });

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "MassTransit Saga PoC",
                Version = "v1",
            });
        });

        return services;
    }

    public static void UsePoCSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("v1/swagger.json", "MassTransit Saga PoC v1");
            options.OAuthClientId("swagger");
            options.OAuthUsePkce();
        });
    }
}
