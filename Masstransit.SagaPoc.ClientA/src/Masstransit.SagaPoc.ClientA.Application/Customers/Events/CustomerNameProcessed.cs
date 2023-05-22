namespace Masstransit.SagaPoc.ClientA.Application.Customers.Events;

public sealed record CustomerNameProcessed : INotification
{
    public Guid Id { get; init; } = Guid.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
}
