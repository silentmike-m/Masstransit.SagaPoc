namespace Masstransit.SagaPoc.Shared.Responses;

public interface ICustomerNameProcessed
{
    string FirstName { get; }
    Guid Id { get; }
    string LastName { get; }
}
