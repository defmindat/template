using MediatR;

namespace EatWise.Common.Domain;

public interface IDomainEvent: INotification
{
    Guid Id { get; }
    DateTime OccurredOnUtc { get; }
}