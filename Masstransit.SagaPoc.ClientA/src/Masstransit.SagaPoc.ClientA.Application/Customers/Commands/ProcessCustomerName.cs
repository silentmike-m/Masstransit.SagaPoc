namespace Masstransit.SagaPoc.ClientA.Application.Customers.Commands;

using Masstransit.SagaPoc.ClientA.Application.Customers.ValueModels;

public sealed record ProcessCustomerName : IRequest
{
    [JsonPropertyName("customer")] public CustomerToProcessName Customer { get; init; } = new();
}
