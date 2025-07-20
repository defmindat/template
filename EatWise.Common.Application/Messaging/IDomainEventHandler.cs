using EatWise.Common.Domain;
using MediatR;

namespace EatWise.Common.Application.Messaging;

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent: IDomainEvent;
