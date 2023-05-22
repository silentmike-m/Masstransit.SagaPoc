namespace Masstransit.SagaPoc.ClientA.Application.Customers.CommandHandlers;

using System.Text.RegularExpressions;
using Masstransit.SagaPoc.ClientA.Application.Customers.Commands;
using Masstransit.SagaPoc.ClientA.Application.Customers.Events;

internal sealed class ProcessCustomerNameHandler : IRequestHandler<ProcessCustomerName>
{
    private const string CUSTOMER_NAME_REGEX = $@"^\s*(?<{FIRST_NAME_REGEX_GROUP}>[^\s]*)(\s+(?<{LAST_NAME_REGEX_GROUP}>.*))?\s*$";
    private const string FIRST_NAME_REGEX_GROUP = "firstName";
    private const string LAST_NAME_REGEX_GROUP = "lastName";

    private readonly ILogger<ProcessCustomerNameHandler> logger;
    private readonly IPublisher mediator;

    public ProcessCustomerNameHandler(ILogger<ProcessCustomerNameHandler> logger, IPublisher mediator)
    {
        this.logger = logger;
        this.mediator = mediator;
    }

    public async Task Handle(ProcessCustomerName request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to process customer  name with id {CustomeId}", request.Customer.Id);

        var nameRegex = new Regex(CUSTOMER_NAME_REGEX);

        var firstName = string.Empty;
        var lastName = string.Empty;

        var isMatch = nameRegex.Match(request.Customer.Name);

        if (isMatch.Success)
        {
            firstName = isMatch.Groups["FIRST_NAME_REGEX_GROUP"].Value;
            lastName = isMatch.Groups["LAST_NAME_REGEX_GROUP"].Value;
        }

        var notification = new CustomerNameProcessed
        {
            FirstName = firstName,
            Id = request.Customer.Id,
            LastName = lastName,
        };

        await this.mediator.Publish(notification, cancellationToken);
    }
}
