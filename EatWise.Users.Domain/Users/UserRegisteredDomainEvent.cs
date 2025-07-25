﻿using EatWise.Common.Domain;

namespace EatWise.Users.Domain.Users;

public sealed class UserRegisteredDomainEvent(Guid userId): DomainEvent
{
    public Guid UserId { get; init; } = userId;
}
