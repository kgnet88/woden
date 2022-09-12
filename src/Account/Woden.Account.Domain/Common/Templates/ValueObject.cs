namespace KgNet88.Woden.Account.Domain.Common.Templates;

/// <summary>
/// Base class for value objects.
/// </summary>
public abstract class ValueObject
{
    /// <summary>
    /// Helper function to define boolean operators for ValueObjects.
    /// </summary>
    /// <param name="left">left operand.</param>
    /// <param name="right">right operand.</param>
    /// <returns><see langword="true"/>, if the objects are equal.</returns>
    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        return !(left is null ^ right is null) && (ReferenceEquals(left, right) || left!.Equals(right));
    }

    /// <summary>
    /// Helper function to define boolean operators for ValueObjects.
    /// </summary>
    /// <param name="left">left operand.</param>
    /// <param name="right">right operand.</param>
    /// <returns><see langword="true"/>, if the objects are not equal.</returns>
    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !EqualOperator(left, right);
    }

    /// <summary>
    /// Abstract function template every derived value object class has to implament. It returns the list of properties,
    /// which are used to identify equality.
    /// </summary>
    /// <returns>list of properties, which decide wether two value objects are equal.</returns>
    protected abstract IEnumerable<object> GetEqualityComponents();

    /// <summary>Determines whether the specified object is equal to the current object.</summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>
    ///   <see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != this.GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;

        return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    /// <summary>Serves as the default hash function and uses only the objects equality components.</summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        return this.GetEqualityComponents()
            .Select(x => (x?.GetHashCode()) ?? 0)
            .Aggregate((x, y) => x ^ y);
    }

    /// <summary>
    /// Creates a memberwise clone of the object and returns it.
    /// </summary>
    /// <returns>Copy of the object.</returns>
    public ValueObject GetCopy()
    {
        return (ValueObject)this.MemberwiseClone();
    }
}