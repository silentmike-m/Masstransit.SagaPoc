namespace Masstransit.SagaPoc.Publisher.Application.Customers.Commands;

using Masstransit.SagaPoc.Publisher.Application.Customers.ValueModels;

public sealed record UpdateCustomerAddress : IRequest
{
    [JsonPropertyName("customer")] public CustomerToUpdateAddress Customer { get; init; } = new();
}
