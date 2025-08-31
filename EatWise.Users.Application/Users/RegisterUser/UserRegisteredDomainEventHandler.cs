using EatWise.Common.Application.EventBus;
using EatWise.Common.Application.Exceptions;
using EatWise.Common.Application.Messaging;
using EatWise.Common.Domain;
using EatWise.Users.Application.Users.GetUser;
using EatWise.Users.Domain.Users;
using EatWise.Users.IntegrationEvents;
using MediatR;

namespace EatWise.Users.Application.Users.RegisterUser;

internal sealed class UserRegisteredDomainEventHandler(ISender sender, IEventBus bus)
    : DomainEventHandler<UserRegisteredDomainEvent>
{
    public override async Task Handle(UserRegisteredDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Result<UserResponse> result = await sender.Send(
            new GetUserQuery(domainEvent.UserId), 
            cancellationToken);

        if (result.IsFailure)
        {
            throw new EatWiseException(nameof(GetUserQuery), result.Error);
        }

        await bus.PublishAsync(new UserRegisteredIntegrationEvent(
            domainEvent.Id,
            domainEvent.OccurredOnUtc,
            result.Value.Id,
            result.Value.Email,
            result.Value.FirstName,
            result.Value.LastName),
            cancellationToken);
    }
}
