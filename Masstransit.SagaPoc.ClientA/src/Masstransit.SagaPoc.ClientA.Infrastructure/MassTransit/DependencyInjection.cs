namespace Masstransit.SagaPoc.ClientA.Infrastructure.MassTransit;

using System.Security.Authentication;
using global::MassTransit;
using Masstransit.SagaPoc.ClientA.Infrastructure.MassTransit.Customers.Consumers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

internal static class DependencyInjection
{
    public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqSettings = configuration.GetSection(RabbitMqSettings.SECTION_NAME).Get<RabbitMqSettings>();

        services.AddMassTransit(busConfiguration =>
        {
            //busConfiguration.AddConsumer<ProcessCustomerConsumer>();
            busConfiguration.AddConsumer(typeof(ProcessCustomerConsumer), typeof(ProcessCustomerConsumerDefinition));

            busConfiguration.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMqSettings.HostName, rabbitMqSettings.Port, rabbitMqSettings.VirtualHost, host =>
                {
                    host.Password(rabbitMqSettings.Password);
                    host.Username(rabbitMqSettings.User);

                    if (rabbitMqSettings.UseSsl)
                    {
                        host.UseSsl(ssl => ssl.Protocol = SslProtocols.Tls12);
                    }
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}

internal sealed class ProcessCustomerConsumerDefinition : ConsumerDefinition<ProcessCustomerConsumer>
{
    public ProcessCustomerConsumerDefinition()
        => this.EndpointName = "process-customer-name";
}
