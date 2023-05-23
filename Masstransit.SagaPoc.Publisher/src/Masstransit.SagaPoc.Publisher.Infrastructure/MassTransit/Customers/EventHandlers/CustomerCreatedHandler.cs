namespace Masstransit.SagaPoc.Publisher.Infrastructure.MassTransit.Customers.EventHandlers;

using global::MassTransit;
using Masstransit.SagaPoc.Publisher.Application.Customers.Events;
using Masstransit.SagaPoc.Publisher.Infrastructure.MassTransit.Customers.Models;

internal sealed class CustomerCreatedHandler : INotificationHandler<CustomerCreated>
{
    private readonly IBus bus;
    private readonly ILogger<CustomerCreatedHandler> logger;
    private readonly IPublishEndpoint publishEndpoint;

    public CustomerCreatedHandler(IBus bus, ILogger<CustomerCreatedHandler> logger, IPublishEndpoint publishEndpoint)
    {
        this.bus = bus;
        this.logger = logger;
        this.publishEndpoint = publishEndpoint;
    }

    public async Task Handle(CustomerCreated notification, CancellationToken cancellationToken)
    {
        using var loggerScope = this.logger.BeginScope("{CustomerId}", notification.Id);

        this.logger.LogInformation("Try to send process customer request");

        var message = new ProcessCustomer
        {
            Address = notification.Address,
            Id = notification.Id,
            Name = notification.Name,
        };

        await this.publishEndpoint.Publish(message, cancellationToken);
        //await this.bus.Send(message, cancellationToken);
    }
}
