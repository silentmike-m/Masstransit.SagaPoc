namespace Masstransit.SagaPoc.Publisher.Application.Customers.ValueModels;

public sealed record CustomerToCreate
{
    [JsonPropertyName("address")] public string Address { get; init; } = string.Empty;
    [JsonPropertyName("id")] public Guid Id { get; init; } = Guid.Empty;
    [JsonPropertyName("name")] public string Name { get; init; } = string.Empty;
}
