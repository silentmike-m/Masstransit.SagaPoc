namespace Masstransit.SagaPoc.Publisher.Application.Customers.Queries;

using Masstransit.SagaPoc.Publisher.Application.Customers.ViewModels;

public sealed record GetCustomers : IRequest<Customers>;
