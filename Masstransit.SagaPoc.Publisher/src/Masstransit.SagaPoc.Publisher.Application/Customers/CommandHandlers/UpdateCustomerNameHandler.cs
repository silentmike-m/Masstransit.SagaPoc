namespace Masstransit.SagaPoc.Publisher.Application.Customers.CommandHandlers;

using Masstransit.SagaPoc.Publisher.Application.Customers.Commands;
using Masstransit.SagaPoc.Publisher.Domain.Repositories;

internal sealed class UpdateCustomerNameHandler : IRequestHandler<UpdateCustomerName>
{
    private const int WAIT_DURATION_IN_SECONDS = 5;

    private readonly ILogger<UpdateCustomerNameHandler> logger;
    private readonly ICustomerRepository repository;

    public UpdateCustomerNameHandler(ILogger<UpdateCustomerNameHandler> logger, ICustomerRepository repository)
    {
        this.logger = logger;
        this.repository = repository;
    }

    public async Task Handle(UpdateCustomerName request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to update customer name with id {CustomerId}", request.Customer.Id);

        var customer = this.repository.Get(request.Customer.Id);

        if (customer is null)
        {
            throw new Exception("Customer not found");
        }

        customer.FirstName = request.Customer.FirstName;
        customer.LastName = request.Customer.LastName;

        Thread.Sleep(TimeSpan.FromSeconds(WAIT_DURATION_IN_SECONDS));

        this.repository.Save(customer);

        await Task.CompletedTask;
    }
}
