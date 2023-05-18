namespace Masstransit.SagaPoc.Publisher.Domain.Repositories;

using Masstransit.SagaPoc.Publisher.Domain.Entities;

public interface ICustomerRepository
{
    public CustomerEntity? Get(Guid id);
    IEnumerable<CustomerEntity> GetAll();
    public void Save(CustomerEntity customer);
}
