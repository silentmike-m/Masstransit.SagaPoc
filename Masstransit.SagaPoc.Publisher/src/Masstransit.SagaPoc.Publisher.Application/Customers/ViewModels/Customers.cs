namespace Masstransit.SagaPoc.Publisher.Application.Customers.ViewModels;

public sealed record Customers
{
    [JsonPropertyName("customers")] public IReadOnlyList<Customer> CustomersList { get; init; } = new List<Customer>();
}
