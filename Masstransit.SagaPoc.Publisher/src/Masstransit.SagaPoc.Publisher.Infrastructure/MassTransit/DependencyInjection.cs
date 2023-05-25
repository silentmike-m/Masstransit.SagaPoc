namespace Masstransit.SagaPoc.Publisher.Infrastructure.MassTransit;

using System.Security.Authentication;
using global::MassTransit;
using Masstransit.SagaPoc.Publisher.Infrastructure.MassTransit.Customers.Sagas;
using Masstransit.SagaPoc.Shared.Extensions;
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
            // busConfiguration.AddConsumer<CustomerAddressProcessedConsumer>();
            // busConfiguration.AddConsumer<CustomerNameProcessedConsumer>();

            busConfiguration.AddSagaStateMachine<CustomerStateMachine, CustomerState>()
                .InMemoryRepository();

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

                cfg.Publish<IProcessCustomer>(e =>
                {
                    e.ExchangeType = ExchangeType.Topic;
                });

                cfg.Send<IProcessCustomer>(message =>
                {
                    message.UseRoutingKeyFormatter(messageContext => messageContext.Message.GetRoutingKex());
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
