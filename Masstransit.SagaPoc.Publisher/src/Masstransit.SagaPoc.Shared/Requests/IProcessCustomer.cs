﻿namespace Masstransit.SagaPoc.Shared.Requests;

public interface IProcessCustomer
{
    const string ADDRESS_ROUTING_KEY = "address";
    const string CUSTOMER_ROUTING_KEY = "customer";
    const string NAME_ROUTING_KEY = "name";

    string Address { get; }
    Guid Id { get; }
    string Name { get; }
}
