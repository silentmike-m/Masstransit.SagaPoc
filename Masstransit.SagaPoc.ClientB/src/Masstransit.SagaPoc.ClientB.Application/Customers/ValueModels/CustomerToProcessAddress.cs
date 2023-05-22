namespace Masstransit.SagaPoc.ClientB.Application.Customers.ValueModels;

public sealed record CustomerToProcessAddress
{
    [JsonPropertyName("address")] public string Address { get; init; } = string.Empty;
    [JsonPropertyName("id")] public Guid Id { get; init; } = Guid.Empty;
}
