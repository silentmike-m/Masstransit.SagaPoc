namespace Masstransit.SagaPoc.ClientA.Infrastructure;

using System.Reflection;
using Masstransit.SagaPoc.ClientA.Infrastructure.MassTransit;
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

        services.AddMassTransit(configuration);

        return services;
    }
}
