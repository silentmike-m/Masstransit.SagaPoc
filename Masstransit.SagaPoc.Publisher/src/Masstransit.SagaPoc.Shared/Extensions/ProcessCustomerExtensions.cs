namespace Masstransit.SagaPoc.Shared.Extensions;

using Masstransit.SagaPoc.Shared.Requests;

public static class ProcessCustomerExtensions
{
    private const string ADDRESS_ROUTING_KEY = "address";
    private const string CUSTOMER_ROUTING_KEY = "customer";
    private const string NAME_ROUTING_KEY = "name";

    public static string GetRoutingKex(this IProcessCustomer self)
    {
        var isAddress = string.IsNullOrWhiteSpace(self.Address) is false;
        var isName = string.IsNullOrWhiteSpace(self.Name) is false;

        return isAddress switch
        {
            true when isName => CUSTOMER_ROUTING_KEY,
            true => ADDRESS_ROUTING_KEY,
            _ => NAME_ROUTING_KEY,
        };
    }
}
