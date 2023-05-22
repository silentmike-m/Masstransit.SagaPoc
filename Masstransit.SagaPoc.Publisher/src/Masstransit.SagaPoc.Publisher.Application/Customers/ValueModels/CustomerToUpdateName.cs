namespace Masstransit.SagaPoc.Publisher.Application.Customers.ValueModels;

public sealed record CustomerToUpdateName
{
    [JsonPropertyName("first_name")] public string FirstName { get; init; } = string.Empty;
    [JsonPropertyName("id")] public Guid Id { get; init; } = Guid.Empty;
    [JsonPropertyName("last_name")] public string LastName { get; init; } = string.Empty;
}
