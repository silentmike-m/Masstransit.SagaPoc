namespace Masstransit.SagaPoc.Publisher.Application.Customers.Commands;

using Masstransit.SagaPoc.Publisher.Application.Customers.ValueModels;

public sealed record UpdateCustomerName : IRequest
{
    [JsonPropertyName("customer")] public CustomerToUpdateName Customer { get; init; } = new();
}
