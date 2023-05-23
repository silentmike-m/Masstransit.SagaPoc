namespace Masstransit.SagaPoc.Publisher.Infrastructure.MassTransit.Customers.Activities;

using global::MassTransit;
using Masstransit.SagaPoc.Publisher.Application.Customers.Commands;
using Masstransit.SagaPoc.Publisher.Application.Customers.ValueModels;
using Masstransit.SagaPoc.Publisher.Infrastructure.MassTransit.Customers.Sagas;
using Masstransit.SagaPoc.Shared.Responses;

internal sealed class CustomerNameProcessedActivity : IStateMachineActivity<CustomerState, ICustomerNameProcessed>
{
    private const string SCOPE_NAME = "processed-customer-name";

    private readonly ILogger<CustomerNameProcessedActivity> logger;
    private readonly ISender mediator;

    public CustomerNameProcessedActivity(ILogger<CustomerNameProcessedActivity> logger, ISender mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }

    public void Accept(StateMachineVisitor visitor)
    {
        visitor.Visit(this);
    }

    public async Task Execute(BehaviorContext<CustomerState, ICustomerNameProcessed> context, IBehavior<CustomerState, ICustomerNameProcessed> next)
    {
        this.logger.LogInformation("Receiver customer with id {CustomerId} name processed message", context.Message.Id);

        var customerToUpdate = new CustomerToUpdateName
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

    public Task Faulted<TException>(BehaviorExceptionContext<CustomerState, ICustomerNameProcessed, TException> context, IBehavior<CustomerState, ICustomerNameProcessed> next) where TException : Exception
        => next.Faulted(context);

    public void Probe(ProbeContext context)
    {
        context.CreateScope(SCOPE_NAME);
    }
}
