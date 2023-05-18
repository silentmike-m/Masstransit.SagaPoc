namespace Masstransit.SagaPoc.Publisher.Application.Customers.ViewModels;

public sealed record Customer
{
    [JsonPropertyName("address")] public string Address { get; init; } = string.Empty;
    [JsonPropertyName("first_name")] public string FirstName { get; init; } = string.Empty;
    [JsonPropertyName("id")] public Guid Id { get; init; } = Guid.Empty;
    [JsonPropertyName("last_name")] public string LastName { get; init; } = string.Empty;
}
