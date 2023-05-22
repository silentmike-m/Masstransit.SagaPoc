namespace Masstransit.SagaPoc.Publisher.WebApi.Controllers;

using Masstransit.SagaPoc.Publisher.Application.Customers.Queries;
using Masstransit.SagaPoc.Publisher.Application.Customers.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController, Authorize, Route("[controller]/[action]")]
public sealed class CustomersController : ApiControllerBase
{
    [HttpGet(Name = "GetAll")]
    public async Task<Customers> GetCurrencies()
        => await this.Mediator.Send(new GetCustomers(), CancellationToken.None);
}
