namespace Masstransit.SagaPoc.Publisher.Infrastructure.MassTransit.Customers.Sagas;

using global::MassTransit;
using Masstransit.SagaPoc.Publisher.Infrastructure.MassTransit.Customers.Activities;
using Masstransit.SagaPoc.Shared.Responses;

internal class CustomerStateMachine : MassTransitStateMachine<CustomerState>
{
    public Event<ICustomerAddressProcessed> CustomerAddressProcessedEvent { get; }
    public State CustomerAddressProcessedState { get; }
    public Event<ICustomerNameProcessed> CustomerNameProcessedEvent { get; }
    public State CustomerNameProcessedState { get; }

    public CustomerStateMachine()
    {
        this.InstanceState(x => x.CurrentState);

        this.Event(() => this.CustomerAddressProcessedEvent,
            x => x.CorrelateById(context => context.Message.Id));

        this.Event(() => this.CustomerNameProcessedEvent,
            x => x.CorrelateById(context => context.Message.Id));

        this.Initially(
            this.When(this.CustomerAddressProcessedEvent).Activity(selector => selector.OfType<CustomerAddressProcessedActivity>()).TransitionTo(this.CustomerAddressProcessedState),
            this.When(this.CustomerNameProcessedEvent).Activity(selector => selector.OfType<CustomerNameProcessedActivity>()).TransitionTo(this.CustomerNameProcessedState)
        );

        this.During(
            this.CustomerAddressProcessedState, this.When(this.CustomerNameProcessedEvent).Activity(selector => selector.OfType<CustomerNameProcessedActivity>()).Finalize()
        );

        this.During(
            this.CustomerNameProcessedState, this.When(this.CustomerAddressProcessedEvent).Activity(selector => selector.OfType<CustomerAddressProcessedActivity>()).Finalize()
        );

        this.SetCompletedWhenFinalized();
    }
}
