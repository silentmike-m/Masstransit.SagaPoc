namespace Masstransit.SagaPoc.Publisher.Infrastructure.MemoryDb.Services;

using Masstransit.SagaPoc.Publisher.Application.Commons.Extensions;
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
        if (this.context.Customers.TryGetValue(id, out var customerJson))
        {
            var customer = customerJson.To<CustomerEntity>();

            return customer;
        }

        return null;
    }

    public IEnumerable<CustomerEntity> GetAll()
    {
        var customers = new List<CustomerEntity>();

        foreach (var (_, customerJson) in this.context.Customers)
        {
            var customer = customerJson.To<CustomerEntity>();

            customers.Add(customer);
        }

        return customers;
    }

    public void Save(CustomerEntity customer)
    {
        var customerJson = customer.ToJson();

        if (this.context.Customers.TryAdd(customer.Id, customerJson) is false)
        {
            this.context.Customers[customer.Id] = customerJson;
        }
    }
}
