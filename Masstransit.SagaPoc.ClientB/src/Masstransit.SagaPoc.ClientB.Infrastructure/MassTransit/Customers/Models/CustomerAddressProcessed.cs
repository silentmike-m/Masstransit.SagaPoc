namespace Masstransit.SagaPoc.ClientB.Infrastructure.MassTransit.Customers.Models;

using Masstransit.SagaPoc.Shared.Responses;

internal sealed record CustomerAddressProcessed : ICustomerAddressProcessed
{
    public string Address { get; init; } = string.Empty;
    public Guid Id { get; init; } = Guid.Empty;
}
