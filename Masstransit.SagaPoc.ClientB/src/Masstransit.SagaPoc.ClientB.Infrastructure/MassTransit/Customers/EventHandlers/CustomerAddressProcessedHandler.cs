namespace Masstransit.SagaPoc.ClientB.Infrastructure.MassTransit.Customers.EventHandlers;

using global::MassTransit;
using Masstransit.SagaPoc.ClientB.Application.Customers.Events;
using Message = Masstransit.SagaPoc.ClientB.Infrastructure.MassTransit.Customers.Models.CustomerAddressProcessed;

internal sealed class CustomerAddressProcessedHandler : INotificationHandler<CustomerAddressProcessed>
{
    private readonly ILogger<CustomerAddressProcessedHandler> logger;
    private readonly IPublishEndpoint publishEndpoint;

    public CustomerAddressProcessedHandler(ILogger<CustomerAddressProcessedHandler> logger, IPublishEndpoint publishEndpoint)
    {
        this.logger = logger;
        this.publishEndpoint = publishEndpoint;
    }

    public async Task Handle(CustomerAddressProcessed notification, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to send processed customer address with id {CustomerId} request", notification.Id);

        var message = new Message
        {
            Address = notification.Address,
            Id = notification.Id,
        };

        await this.publishEndpoint.Publish(message, cancellationToken);
    }
}
