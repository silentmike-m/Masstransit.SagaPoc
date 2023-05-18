namespace Masstransit.SagaPoc.Publisher.Application.Customers.Commands;

using Masstransit.SagaPoc.Publisher.Application.Customers.ValueModels;

public sealed record CreateCustomer : IRequest
{
    [JsonPropertyName("customer")] public CustomerToCreate Customer { get; init; } = new();
}
