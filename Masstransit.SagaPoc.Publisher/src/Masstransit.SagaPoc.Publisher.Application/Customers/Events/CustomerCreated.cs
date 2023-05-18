namespace Masstransit.SagaPoc.Publisher.Application.Customers.Events;

public sealed record CustomerCreated : INotification
{
    public string Address { get; init; } = string.Empty;
    public Guid Id { get; init; } = Guid.Empty;
    public string Name { get; init; } = string.Empty;
}
