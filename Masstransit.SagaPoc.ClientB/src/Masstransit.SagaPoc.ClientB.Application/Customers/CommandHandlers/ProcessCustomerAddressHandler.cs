namespace Masstransit.SagaPoc.ClientB.Application.Customers.CommandHandlers;

using Masstransit.SagaPoc.ClientB.Application.Customers.Commands;
using Masstransit.SagaPoc.ClientB.Application.Customers.Events;

internal sealed class ProcessCustomerAddressHandler : IRequestHandler<ProcessCustomerAddress>
{
    private readonly ILogger<ProcessCustomerAddressHandler> logger;
    private readonly IPublisher mediator;

    public ProcessCustomerAddressHandler(ILogger<ProcessCustomerAddressHandler> logger, IPublisher mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }

    public async Task Handle(ProcessCustomerAddress request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to process customer  address with id {CustomeId}", request.Customer.Id);

        var notification = new CustomerAddressProcessed
        {
            Address = request.Customer.Address,
            Id = request.Customer.Id,
        };

        await this.mediator.Publish(notification, cancellationToken);
    }
}
