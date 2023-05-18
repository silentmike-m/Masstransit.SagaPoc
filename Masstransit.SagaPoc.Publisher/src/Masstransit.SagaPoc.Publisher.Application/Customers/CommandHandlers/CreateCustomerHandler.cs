namespace Masstransit.SagaPoc.Publisher.Application.Customers.CommandHandlers;

using Masstransit.SagaPoc.Publisher.Application.Customers.Commands;
using Masstransit.SagaPoc.Publisher.Application.Customers.Events;
using Masstransit.SagaPoc.Publisher.Domain.Entities;
using Masstransit.SagaPoc.Publisher.Domain.Repositories;

internal sealed class CreateCustomerHandler : IRequestHandler<CreateCustomer>
{
    private readonly ILogger<CreateCustomerHandler> logger;
    private readonly IPublisher mediator;
    private readonly ICustomerRepository repository;

    public CreateCustomerHandler(ILogger<CreateCustomerHandler> logger, IPublisher mediator, ICustomerRepository repository)
    {
        this.logger = logger;
        this.mediator = mediator;
        this.repository = repository;
    }

    public async Task Handle(CreateCustomer request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to create customer with id {CustomerId}", request.Customer.Id);

        var customer = this.repository.Get(request.Customer.Id);

        if (customer is not null)
        {
            throw new ArgumentException($"Customer with id {request.Customer.Id} already exists");
        }

        customer = new CustomerEntity(request.Customer.Id);

        this.repository.Save(customer);

        var notification = new CustomerCreated
        {
            Address = request.Customer.Address,
            Id = request.Customer.Id,
            Name = request.Customer.Name,
        };

        await this.mediator.Publish(notification, cancellationToken);
    }
}
