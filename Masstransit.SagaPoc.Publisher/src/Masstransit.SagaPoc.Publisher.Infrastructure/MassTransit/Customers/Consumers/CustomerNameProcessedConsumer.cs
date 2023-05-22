namespace Masstransit.SagaPoc.Publisher.Infrastructure.MassTransit.Customers.Consumers;

using global::MassTransit;
using Masstransit.SagaPoc.Publisher.Application.Customers.Commands;
using Masstransit.SagaPoc.Publisher.Application.Customers.ValueModels;
using Masstransit.SagaPoc.Shared.Responses;

internal sealed class CustomerNameProcessedConsumer : IConsumer<ICustomerNameProcessed>
{
    private readonly ILogger<CustomerNameProcessedConsumer> logger;
    private readonly ISender mediator;

    public CustomerNameProcessedConsumer(ILogger<CustomerNameProcessedConsumer> logger, ISender mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ICustomerNameProcessed> context)
    {
        this.logger.LogInformation("Receiver customer with id {CustomerId} name processed message", context.Message.Id);

        var customerToUpdate = new CustomerToUpdateName()
        {
            FirstName = context.Message.FirstName,
            Id = context.Message.Id,
            LastName = context.Message.LastName,
        };

        var request = new UpdateCustomerName
        {
            Customer = customerToUpdate,
        };

        await this.mediator.Send(request, context.CancellationToken);
    }
}
