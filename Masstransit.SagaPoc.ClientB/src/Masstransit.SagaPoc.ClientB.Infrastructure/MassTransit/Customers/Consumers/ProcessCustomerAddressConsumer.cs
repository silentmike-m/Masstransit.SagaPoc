﻿namespace Masstransit.SagaPoc.ClientB.Infrastructure.MassTransit.Customers.Consumers;

using global::MassTransit;
using Masstransit.SagaPoc.ClientB.Application.Customers.Commands;
using Masstransit.SagaPoc.ClientB.Application.Customers.ValueModels;
using Masstransit.SagaPoc.Shared.Requests;

internal sealed class ProcessCustomerConsumer : IConsumer<IProcessCustomer>
{
    private readonly ILogger<ProcessCustomerConsumer> logger;
    private readonly ISender mediator;

    public ProcessCustomerConsumer(ILogger<ProcessCustomerConsumer> logger, ISender mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }

    public async Task Consume(ConsumeContext<IProcessCustomer> context)
    {
        this.logger.LogInformation("Received process customer with id {CustomerId} request", context.Message.Id);

        var customerToProcess = new CustomerToProcessAddress
        {
            Address = context.Message.Address,
            Id = context.Message.Id,
        };

        var request = new ProcessCustomerAddress
        {
            Customer = customerToProcess,
        };

        await this.mediator.Send(request, context.CancellationToken);
    }
}
