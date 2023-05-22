namespace Masstransit.SagaPoc.Publisher.Infrastructure.Customers.QueryHandlers;

using Masstransit.SagaPoc.Publisher.Application.Customers.Queries;
using Masstransit.SagaPoc.Publisher.Application.Customers.ViewModels;
using Masstransit.SagaPoc.Publisher.Infrastructure.Customers.Interfaces;

internal sealed class GetCustomersHandler : IRequestHandler<GetCustomers, Customers>
{
    private readonly ILogger<GetCustomersHandler> logger;
    private readonly ICustomerReadService readService;

    public GetCustomersHandler(ILogger<GetCustomersHandler> logger, ICustomerReadService readService)
    {
        this.logger = logger;
        this.readService = readService;
    }

    public async Task<Customers> Handle(GetCustomers request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to get all customers");

        var result = this.readService.GetCustomers();

        return await Task.FromResult(result);
    }
}
