namespace Masstransit.SagaPoc.Publisher.Infrastructure.MemoryDb.Services;

using Masstransit.SagaPoc.Publisher.Infrastructure.MemoryDb.Interfaces;
using CustomerId = Guid;

internal sealed class Context : IContext
{
    public Dictionary<CustomerId, string> Customers { get; private set; }

    public Context()
        => this.Customers = new Dictionary<CustomerId, string>();
}
