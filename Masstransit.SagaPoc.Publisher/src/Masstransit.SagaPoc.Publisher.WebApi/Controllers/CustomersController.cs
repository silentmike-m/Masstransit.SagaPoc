namespace Masstransit.SagaPoc.Publisher.WebApi.Controllers;

using Masstransit.SagaPoc.Publisher.Application.Customers.Commands;
using Masstransit.SagaPoc.Publisher.Application.Customers.Queries;
using Masstransit.SagaPoc.Publisher.Application.Customers.ViewModels;
using Microsoft.AspNetCore.Mvc;

[ApiController, Route("[controller]/[action]")]
public sealed class CustomersController : ApiControllerBase
{
    [HttpPost(Name = "Create")]
    public async Task<IActionResult> CreateCustomer(CreateCustomer request, CancellationToken cancellationToken = default)
    {
        await this.Mediator.Send(request, cancellationToken);

        return await Task.FromResult(this.Ok());
    }

    [HttpGet(Name = "GetAll")]
    public async Task<Customers> GetCustomers(CancellationToken cancellationToken = default)
        => await this.Mediator.Send(new GetCustomers(), cancellationToken);
}
