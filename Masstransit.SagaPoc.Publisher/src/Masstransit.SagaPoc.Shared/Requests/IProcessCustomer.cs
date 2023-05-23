namespace Masstransit.SagaPoc.Shared.Requests;

public interface IProcessCustomer
{
    string Address { get; }
    Guid Id { get; }
    string Name { get; }
}
