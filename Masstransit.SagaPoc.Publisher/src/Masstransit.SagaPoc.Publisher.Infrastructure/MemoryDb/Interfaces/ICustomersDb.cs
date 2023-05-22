namespace Masstransit.SagaPoc.Publisher.Infrastructure.MemoryDb.Interfaces;

using CustomerId = Guid;

internal interface IContext
{
    Dictionary<CustomerId, string> Customers { get; }
}
