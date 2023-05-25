namespace Masstransit.SagaPoc.ClientB.Infrastructure.MassTransit;

using System.Security.Authentication;
using global::MassTransit;
using Masstransit.SagaPoc.ClientB.Infrastructure.MassTransit.Customers.Consumers;
using Masstransit.SagaPoc.Shared.Requests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

internal static class DependencyInjection
{
    public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqSettings = configuration.GetSection(RabbitMqSettings.SECTION_NAME).Get<RabbitMqSettings>();

        services.AddMassTransit(busConfiguration =>
        {
            busConfiguration.AddConsumer<ProcessCustomerConsumer>();

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

                cfg.ReceiveEndpoint(endpointConfigurator =>
                {
                    endpointConfigurator.ConfigureConsumeTopology = false;
                    endpointConfigurator.ConfigureConsumer<ProcessCustomerConsumer>(context);

                    endpointConfigurator.Bind<IProcessCustomer>(configurator =>
                    {
                        configurator.RoutingKey = IProcessCustomer.ADDRESS_ROUTING_KEY;
                        configurator.ExchangeType = ExchangeType.Topic;
                    });
                });

                cfg.ReceiveEndpoint(endpointConfigurator =>
                {
                    endpointConfigurator.ConfigureConsumeTopology = false;
                    endpointConfigurator.ConfigureConsumer<ProcessCustomerConsumer>(context);

                    endpointConfigurator.Bind<IProcessCustomer>(configurator =>
                    {
                        configurator.RoutingKey = IProcessCustomer.CUSTOMER_ROUTING_KEY;
                        configurator.ExchangeType = ExchangeType.Topic;
                    });
                });
            });
        });

        return services;
    }
}
