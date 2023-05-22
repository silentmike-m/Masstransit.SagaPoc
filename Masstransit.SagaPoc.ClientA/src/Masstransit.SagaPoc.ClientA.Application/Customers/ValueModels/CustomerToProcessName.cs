namespace Masstransit.SagaPoc.ClientA.Application.Customers.ValueModels;

public sealed record CustomerToProcessName
{
    [JsonPropertyName("id")] public Guid Id { get; init; } = Guid.Empty;
    [JsonPropertyName("name")] public string Name { get; init; } = string.Empty;
}
