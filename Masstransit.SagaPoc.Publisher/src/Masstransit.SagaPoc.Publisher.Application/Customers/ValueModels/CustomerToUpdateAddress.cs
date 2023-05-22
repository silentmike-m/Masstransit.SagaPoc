namespace Masstransit.SagaPoc.Publisher.Application.Customers.ValueModels;

public sealed record CustomerToUpdateAddress
{
    [JsonPropertyName("address")] public string Address { get; init; } = string.Empty;
    [JsonPropertyName("id")] public Guid Id { get; init; } = Guid.Empty;
}
