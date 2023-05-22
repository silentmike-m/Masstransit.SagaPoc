namespace Masstransit.SagaPoc.ClientB.Application.Customers.Commands;

using Masstransit.SagaPoc.ClientB.Application.Customers.ValueModels;

public sealed record ProcessCustomerAddress : IRequest
{
    [JsonPropertyName("customer")] public CustomerToProcessAddress Customer { get; init; } = new();
}
