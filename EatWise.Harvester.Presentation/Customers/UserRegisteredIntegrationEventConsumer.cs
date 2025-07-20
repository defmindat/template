using EatWise.Common.Application.Exceptions;
using EatWise.Common.Domain;
using EatWise.Harvester.Application.Customers.CreateCustomer;
using EatWise.Users.IntegrationEvents;
using MassTransit;
using MediatR;

namespace EatWise.Harvester.Presentation.Customers;

public sealed class UserRegisteredIntegrationEventConsumer(ISender sender)
    : IConsumer<UserRegisteredIntegrationEvent>
{
    public async Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
    {
        Result result = await sender.Send(
            new CreateCustomerCommand(
                context.Message.UserId,
                context.Message.Email,
                context.Message.FirstName,
                context.Message.LastName),
            context.CancellationToken
            );

        if (result.IsFailure)
        {
            throw new EatWiseException(nameof(CreateCustomerCommand), result.Error);
        }
    }
}
