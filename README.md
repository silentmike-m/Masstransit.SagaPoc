# Masstransit.SagaPoc

## Req

* .NET 6
* Docker
* Project Tye

## Run rabbitMQ and Seq

```console
docker-compose up
```

## Run application

```console
tye run
```

### How it works
* Publisher publishes IProcessCustomer message with topi: customer, address or name. 
* ClientA receives message with topic customer or name. It will process customer name and publish ICustomerNameProcessed with first and last name.
* ClientB receives message with topic customer or address. It will process customer address and publish ICustomerAddressProcessed.
* Publisher waits for both messages. With saga data are save, wiouth saga data will be overwritten.


### To disable saga
In Publisher MassTransit/DependencyInjection change code to

```csharp
            busConfiguration.AddConsumer<CustomerAddressProcessedConsumer>();
            busConfiguration.AddConsumer<CustomerNameProcessedConsumer>();

            // busConfiguration.AddSagaStateMachine<CustomerStateMachine, CustomerState>()
            //     .InMemoryRepository();
```
