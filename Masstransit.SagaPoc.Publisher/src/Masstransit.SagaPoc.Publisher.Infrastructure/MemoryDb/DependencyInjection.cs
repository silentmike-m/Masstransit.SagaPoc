namespace Masstransit.SagaPoc.Publisher.Infrastructure.MemoryDb;

using Masstransit.SagaPoc.Publisher.Domain.Repositories;
using Masstransit.SagaPoc.Publisher.Infrastructure.Customers.Interfaces;
using Masstransit.SagaPoc.Publisher.Infrastructure.MemoryDb.Interfaces;
using Masstransit.SagaPoc.Publisher.Infrastructure.MemoryDb.Services;
using Microsoft.Extensions.DependencyInjection;

internal static class DependencyInjection
{
    public static IServiceCollection AddMemoryDb(this IServiceCollection services)
    {
        services.AddSingleton(typeof(IContext), new Context());

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICustomerReadService, CustomerReadService>();

        return services;
    }
}
