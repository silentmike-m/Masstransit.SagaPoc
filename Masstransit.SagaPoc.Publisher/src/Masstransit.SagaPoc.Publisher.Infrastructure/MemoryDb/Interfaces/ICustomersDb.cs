namespace Masstransit.SagaPoc.Publisher.Infrastructure.MemoryDb.Interfaces;

using Masstransit.SagaPoc.Publisher.Domain.Entities;
using CustomerId = Guid;

internal interface IContext
{
    Dictionary<CustomerId, CustomerEntity> Customers { get; }
}
