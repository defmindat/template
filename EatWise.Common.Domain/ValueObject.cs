﻿namespace EatWise.Common.Domain;

public abstract class ValueObject
{
    #pragma warning disable S3875
    // The code that's violating the rule is on this line.
    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        if (Equals(left, null))
        {
            return Equals(right, null);
        }

        return left.Equals(right);
    }
    #pragma warning restore CA1046

    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !(left == right);
    }

    protected abstract IEnumerable<object?> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents().Select(component => component != null ? component.GetHashCode() : 0)
            .Aggregate((componentA, componentB) => componentA ^ componentB);
    }
}
