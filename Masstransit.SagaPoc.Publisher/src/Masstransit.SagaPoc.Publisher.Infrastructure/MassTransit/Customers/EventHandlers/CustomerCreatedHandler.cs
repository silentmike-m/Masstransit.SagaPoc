namespace Masstransit.SagaPoc.Publisher.Infrastructure.MassTransit.Customers.EventHandlers;

using global::MassTransit;
using Masstransit.SagaPoc.Publisher.Application.Customers.Events;
using Masstransit.SagaPoc.Publisher.Infrastructure.MassTransit.Customers.Models;
using Masstransit.SagaPoc.Shared.Requests;

internal sealed class CustomerCreatedHandler : INotificationHandler<CustomerCreated>
{
    private readonly ILogger<CustomerCreatedHandler> logger;
    private readonly IPublishEndpoint publishEndpoint;

    public CustomerCreatedHandler(ILogger<CustomerCreatedHandler> logger, IPublishEndpoint publishEndpoint)
    {
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

        await this.publishEndpoint.Publish<IProcessCustomer>(message, cancellationToken);
    }
}
