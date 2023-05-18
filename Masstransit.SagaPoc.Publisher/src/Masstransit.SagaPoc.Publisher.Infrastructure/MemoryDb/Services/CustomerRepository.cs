namespace Masstransit.SagaPoc.Publisher.Infrastructure.MemoryDb.Services;

using Masstransit.SagaPoc.Publisher.Domain.Entities;
using Masstransit.SagaPoc.Publisher.Domain.Repositories;
using Masstransit.SagaPoc.Publisher.Infrastructure.MemoryDb.Interfaces;

internal sealed class CustomerRepository : ICustomerRepository
{
    private readonly IContext context;

    public CustomerRepository(IContext context)
        => this.context = context;

    public CustomerEntity? Get(Guid id)
    {
        if (this.context.Customers.TryGetValue(id, out var customer))
        {
            return customer;
        }

        return null;
    }

    public IEnumerable<CustomerEntity> GetAll()
        => this.context.Customers.Values;

    public void Save(CustomerEntity customer)
    {
        if (this.context.Customers.TryAdd(customer.Id, customer) is false)
        {
            this.context.Customers[customer.Id] = customer;
        }
    }
}
