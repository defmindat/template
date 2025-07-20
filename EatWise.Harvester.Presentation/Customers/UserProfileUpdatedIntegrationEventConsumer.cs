using EatWise.Common.Application.Exceptions;
using EatWise.Common.Domain;
using EatWise.Harvester.Application.Customers.UpdateCustomer;
using EatWise.Users.IntegrationEvents;
using MassTransit;
using MediatR;

namespace EatWise.Harvester.Presentation.Customers;

public sealed class UserProfileUpdatedIntegrationEventConsumer(ISender sender)
    : IConsumer<UserProfileUpdatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<UserProfileUpdatedIntegrationEvent> context)
    {
        Result result = await sender.Send(
            new UpdateCustomerCommand(
                context.Message.UserId,
                context.Message.FirstName,
                context.Message.LastName),
            context.CancellationToken);

        if (result.IsFailure)
        {
            throw new EatWiseException(nameof(UpdateCustomerCommand), result.Error);
        }
    }
}
