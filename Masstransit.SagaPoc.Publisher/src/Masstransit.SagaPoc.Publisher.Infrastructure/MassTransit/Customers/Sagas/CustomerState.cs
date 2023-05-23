namespace Masstransit.SagaPoc.Publisher.Infrastructure.MassTransit.Customers.Sagas;

using global::MassTransit;

internal sealed class CustomerState : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }
}
