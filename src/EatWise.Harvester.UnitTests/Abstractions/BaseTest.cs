﻿using Bogus;
using EatWise.Common.Domain;

namespace EatWise.Harvester.UnitTests.Abstractions;
#pragma warning disable CA1515
public abstract class BaseTest
{
    protected static readonly Faker Faker = new();

    public static T AssertDomainEventWasPublished<T>(Entity entity)
        where T : IDomainEvent
    {
        T? domainEvent = entity.DomainEvents.OfType<T>().SingleOrDefault();

        if (domainEvent is null)
        {
            throw new Exception($"{typeof(T).Name} was not published.");
        }
        
        return domainEvent;
    }
}
#pragma warning restore CA1515
