namespace Masstransit.SagaPoc.Publisher.Application.Customers.CommandHandlers;

using Masstransit.SagaPoc.Publisher.Application.Customers.Commands;
using Masstransit.SagaPoc.Publisher.Domain.Repositories;

internal sealed class UpdateCustomerAddressHandler : IRequestHandler<UpdateCustomerAddress>
{
    private const int WAIT_DURATION_IN_SECONDS = 5;

    private readonly ILogger<UpdateCustomerAddressHandler> logger;
    private readonly ICustomerRepository repository;

    public UpdateCustomerAddressHandler(ILogger<UpdateCustomerAddressHandler> logger, ICustomerRepository repository)
    {
        this.logger = logger;
        this.repository = repository;
    }

    public async Task Handle(UpdateCustomerAddress request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to update customer address with id {CustomerId}", request.Customer.Id);

        var customer = this.repository.Get(request.Customer.Id);

        if (customer is null)
        {
            throw new Exception("Customer not found");
        }

        customer.Address = request.Customer.Address;

        Thread.Sleep(TimeSpan.FromSeconds(WAIT_DURATION_IN_SECONDS));

        this.repository.Save(customer);

        await Task.CompletedTask;
    }
}
