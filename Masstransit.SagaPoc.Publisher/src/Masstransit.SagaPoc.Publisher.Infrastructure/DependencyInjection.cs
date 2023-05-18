namespace Masstransit.SagaPoc.Publisher.Infrastructure;

using System.Reflection;
using Masstransit.SagaPoc.Publisher.Infrastructure.MassTransit;
using Masstransit.SagaPoc.Publisher.Infrastructure.MemoryDb;
using Masstransit.SagaPoc.Publisher.Infrastructure.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddMemoryDb();
        services.AddMassTransit(configuration);
        services.AddPoCSwagger(configuration);

        return services;
    }

    public static void UseInfrastructure(this IApplicationBuilder app)
    {
        app.UsePoCSwagger();
    }
}
