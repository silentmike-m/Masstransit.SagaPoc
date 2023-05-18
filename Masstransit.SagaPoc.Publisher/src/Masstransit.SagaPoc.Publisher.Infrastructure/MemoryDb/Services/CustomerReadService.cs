namespace Masstransit.SagaPoc.Publisher.Infrastructure.MemoryDb.Services;

using Masstransit.SagaPoc.Publisher.Application.Customers.ViewModels;
using Masstransit.SagaPoc.Publisher.Domain.Repositories;
using Masstransit.SagaPoc.Publisher.Infrastructure.Customers.Interfaces;

internal sealed class CustomerReadService : ICustomerReadService
{
    private readonly ICustomerRepository repository;

    public CustomerReadService(ICustomerRepository repository)
        => this.repository = repository;

    public Customers GetCustomers()
    {
        var customers = new List<Customer>();

        foreach (var customerEntity in this.repository.GetAll())
        {
            var customer = new Customer
            {
                Address = customerEntity.Address,
                FirstName = customerEntity.FirstName,
                Id = customerEntity.Id,
                LastName = customerEntity.LastName,
            };

            customers.Add(customer);
        }

        var result = new Customers
        {
            CustomersList = customers,
        };

        return result;
    }
}
