namespace Masstransit.SagaPoc.ClientA.Infrastructure.MassTransit.Customers.EventHandlers;

using global::MassTransit;
using Masstransit.SagaPoc.ClientA.Application.Customers.Events;
using Message = Masstransit.SagaPoc.ClientA.Infrastructure.MassTransit.Customers.Models.CustomerNameProcessed;

internal sealed class CustomerNameProcessedHandler : INotificationHandler<CustomerNameProcessed>
{
    private readonly ILogger<CustomerNameProcessedHandler> logger;
    private readonly IPublishEndpoint publishEndpoint;

    public CustomerNameProcessedHandler(ILogger<CustomerNameProcessedHandler> logger, IPublishEndpoint publishEndpoint)
    {
        this.logger = logger;
        this.publishEndpoint = publishEndpoint;
    }

    public async Task Handle(CustomerNameProcessed notification, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to send processed customer name with id {CustomerId} request", notification.Id);

        var message = new Message
        {
            FirstName = notification.FirstName,
            Id = notification.Id,
            LastName = notification.LastName,
        };

        await this.publishEndpoint.Publish(message, cancellationToken);
    }
}
