namespace Masstransit.SagaPoc.Publisher.Infrastructure.MassTransit.Customers.Models;

using Masstransit.SagaPoc.Shared.Requests;

internal sealed record ProcessCustomer : IProcessCustomer
{
    public string Address { get; init; } = string.Empty;
    public Guid Id { get; init; } = Guid.Empty;
    public string Name { get; init; } = string.Empty;
}
