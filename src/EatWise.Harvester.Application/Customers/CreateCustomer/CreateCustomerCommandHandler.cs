using EatWise.Common.Application.Messaging;
using EatWise.Common.Domain;
using EatWise.Customers;
using EatWise.Harvester.Application.Abstractions.Data;

namespace EatWise.Harvester.Application.Customers.CreateCustomer;

internal sealed class CreateCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCustomerCommand>
{
    public async Task<Result> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = Customer.Create(command.CustomerId, command.Email, command.FirstName, command.LastName);
        
        customerRepository.Insert(customer);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
