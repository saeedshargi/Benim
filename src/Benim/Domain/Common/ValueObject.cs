namespace Benim.Domain.Common;

public abstract class ValueObject: IEquatable<ValueObject>
{
    protected abstract IEnumerable<object> GetValues();

    private static bool EqualOperator(ValueObject left, ValueObject right)
    {
        if (ReferenceEquals(left,null) ^ ReferenceEquals(right,null))
        {
            return false;
        }

        if ((left is null && right is not null) || (left is not null && right is null))
        {
            return false;
        }

        if (left is null && right is null)
        {
            return true;
        }

        return ReferenceEquals(left, right) || left!.Equals(right);
    }

    private static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !EqualOperator(left, right);
    }

    public static bool operator ==(ValueObject one, ValueObject two)
    {
        return EqualOperator(one, two);
    }

    public static bool operator !=(ValueObject one, ValueObject two)
    {
        return NotEqualOperator(one, two);
    }

    public bool Equals(ValueObject? other)
    {
        return other is not null && other.GetType() == GetType() && ValuesAreEqual(other);
    }

    public override bool Equals(object? obj)
    {
        return obj is ValueObject valueObject && ValuesAreEqual(valueObject);
    }

    public override int GetHashCode()
    {
        return GetValues().Aggregate(default(int), HashCode.Combine);
    }

    private bool ValuesAreEqual(ValueObject other)
    {
        return GetValues().SequenceEqual(other.GetValues());
    }
}