namespace Masstransit.SagaPoc.ClientB.Application.Customers.Events;

public sealed record CustomerAddressProcessed : INotification
{
    public string Address { get; init; } = string.Empty;
    public Guid Id { get; init; } = Guid.Empty;
}
