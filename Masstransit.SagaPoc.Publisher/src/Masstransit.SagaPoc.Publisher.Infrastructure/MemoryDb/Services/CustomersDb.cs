namespace Masstransit.SagaPoc.Publisher.Infrastructure.MemoryDb.Services;

using Masstransit.SagaPoc.Publisher.Domain.Entities;
using CustomerId = Guid;

internal sealed class Context
{
    public Dictionary<CustomerId, CustomerEntity> Customers { get; private set; }

    public Context()
        => this.Customers = new Dictionary<CustomerId, CustomerEntity>();
}
