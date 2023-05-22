namespace Masstransit.SagaPoc.Shared.Responses;

public interface ICustomerAddressProcessed
{
    string Address { get; }
    Guid Id { get; }
}
