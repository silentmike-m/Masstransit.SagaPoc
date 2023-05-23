namespace Masstransit.SagaPoc.Publisher.Infrastructure.MassTransit.Customers.Activities;

using global::MassTransit;
using Masstransit.SagaPoc.Publisher.Application.Customers.Commands;
using Masstransit.SagaPoc.Publisher.Application.Customers.ValueModels;
using Masstransit.SagaPoc.Publisher.Infrastructure.MassTransit.Customers.Sagas;
using Masstransit.SagaPoc.Shared.Responses;

internal sealed class CustomerAddressProcessedActivity : IStateMachineActivity<CustomerState, ICustomerAddressProcessed>
{
    private const string SCOPE_NAME = "processed-customer-address";

    private readonly ILogger<CustomerAddressProcessedActivity> logger;
    private readonly ISender mediator;

    public CustomerAddressProcessedActivity(ILogger<CustomerAddressProcessedActivity> logger, ISender mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }

    public void Accept(StateMachineVisitor visitor)
    {
        visitor.Visit(this);
    }

    public async Task Execute(BehaviorContext<CustomerState, ICustomerAddressProcessed> context, IBehavior<CustomerState, ICustomerAddressProcessed> next)
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

    public Task Faulted<TException>(BehaviorExceptionContext<CustomerState, ICustomerAddressProcessed, TException> context, IBehavior<CustomerState, ICustomerAddressProcessed> next) where TException : Exception
        => next.Faulted(context);

    public void Probe(ProbeContext context)
    {
        context.CreateScope(SCOPE_NAME);
    }
}
