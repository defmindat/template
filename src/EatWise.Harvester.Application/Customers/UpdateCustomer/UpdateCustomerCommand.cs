using EatWise.Common.Application.Messaging;

namespace EatWise.Harvester.Application.Customers.UpdateCustomer;

public sealed record UpdateCustomerCommand(Guid CustomerId, string FirstName, string LastName) : ICommand;
