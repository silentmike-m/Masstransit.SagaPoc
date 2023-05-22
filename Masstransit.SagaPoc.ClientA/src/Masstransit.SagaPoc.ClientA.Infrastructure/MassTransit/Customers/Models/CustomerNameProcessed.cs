namespace Masstransit.SagaPoc.ClientA.Infrastructure.MassTransit.Customers.Models;

using Masstransit.SagaPoc.Shared.Responses;

internal sealed record CustomerNameProcessed : ICustomerNameProcessed
{
    public string FirstName { get; init; } = string.Empty;
    public Guid Id { get; init; } = Guid.Empty;
    public string LastName { get; init; } = string.Empty;
}
