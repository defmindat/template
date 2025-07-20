using EatWise.Common.Domain;

namespace EatWise.Common.Application.Messaging;

public abstract class DomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    public abstract Task Handle(TDomainEvent notification, CancellationToken cancellationToken);

    public Task Handle(IDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return Handle((TDomainEvent)domainEvent, cancellationToken);
    }
}
