namespace Masstransit.SagaPoc.Publisher.Infrastructure.Customers.Interfaces;

using Masstransit.SagaPoc.Publisher.Application.Customers.ViewModels;

internal interface ICustomerReadService
{
    Customers GetCustomers();
}
