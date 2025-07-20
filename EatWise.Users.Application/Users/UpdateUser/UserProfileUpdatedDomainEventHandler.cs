using EatWise.Common.Application.EventBus;
using EatWise.Common.Application.Messaging;
using EatWise.Users.Domain.Users;
using EatWise.Users.IntegrationEvents;

namespace EatWise.Users.Application.Users.UpdateUser;

internal sealed class UserProfileUpdatedDomainEventHandler(IEventBus eventBus)
    : DomainEventHandler<UserProfileUpdatedDomainEvent>
{
    public override async Task Handle(UserProfileUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await eventBus.PublishAsync(
            new UserProfileUpdatedIntegrationEvent(
                notification.Id,
                notification.OccurredOnUtc,
                notification.UserId,
                notification.FirstName,
                notification.LastName),
            cancellationToken);
    }
}
