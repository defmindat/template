using EatWise.Common.Application.Messaging;

namespace EatWise.Harvester.Application.Customers.CreateCustomer;

public sealed record CreateCustomerCommand(Guid CustomerId, string Email, string FirstName, string LastName)
    : ICommand;
