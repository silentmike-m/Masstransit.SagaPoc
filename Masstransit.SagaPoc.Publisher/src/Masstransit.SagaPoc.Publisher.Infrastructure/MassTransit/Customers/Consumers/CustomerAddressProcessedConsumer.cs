namespace Masstransit.SagaPoc.Publisher.Infrastructure.MassTransit.Customers.Consumers;

using global::MassTransit;
using Masstransit.SagaPoc.Publisher.Application.Customers.Commands;
using Masstransit.SagaPoc.Publisher.Application.Customers.ValueModels;
using Masstransit.SagaPoc.Shared.Responses;

internal sealed class CustomerAddressProcessedConsumer : IConsumer<ICustomerAddressProcessed>
{
    private readonly ILogger<CustomerAddressProcessedConsumer> logger;
    private readonly ISender mediator;

    public CustomerAddressProcessedConsumer(ILogger<CustomerAddressProcessedConsumer> logger, ISender mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ICustomerAddressProcessed> context)
    {
        this.logger.LogInformation("Receiver customer with id {CustomerId} address processed message", context.Message.Id);

        var customerToUpdate = new CustomerToUpdateAddress
        {
            Address = context.Message.Address,
            Id = context.Message.Id,
        };

        var request = new UpdateCustomerAddress
        {
            Customer = customerToUpdate,
        };

        await this.mediator.Send(request, context.CancellationToken);
    }
}
