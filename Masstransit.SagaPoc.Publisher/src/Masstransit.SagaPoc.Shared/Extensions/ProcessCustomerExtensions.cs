namespace Masstransit.SagaPoc.Shared.Extensions;

using Masstransit.SagaPoc.Shared.Requests;

public static class ProcessCustomerExtensions
{
    public static string GetRoutingKex(this IProcessCustomer self)
    {
        var isAddress = string.IsNullOrWhiteSpace(self.Address) is false;
        var isName = string.IsNullOrWhiteSpace(self.Name) is false;

        return isAddress switch
        {
            true when isName => IProcessCustomer.CUSTOMER_ROUTING_KEY,
            true => IProcessCustomer.ADDRESS_ROUTING_KEY,
            _ => IProcessCustomer.NAME_ROUTING_KEY,
        };
    }
}
