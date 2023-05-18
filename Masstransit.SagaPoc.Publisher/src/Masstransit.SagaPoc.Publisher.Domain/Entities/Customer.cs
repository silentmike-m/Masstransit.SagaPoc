namespace Masstransit.SagaPoc.Publisher.Domain.Entities;

public sealed class CustomerEntity
{
    public Guid Id { get; private set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public CustomerEntity(Guid id)
    {
        this.Id = id;
    }
}
