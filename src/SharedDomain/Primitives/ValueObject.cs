namespace SharedDomain.Primitives
{
    /// <summary>
    /// Provides a base class for Value Objects in the domain.
    /// </summary>
    /// <remarks>
    /// Value Objects do not have a unique identity (No Id) and are defined by the
    /// values of their properties. They are immutable and follow value-based equality.
    /// </remarks>
    public abstract class ValueObject
    {
        /// <summary>
        /// When implemented, returns the components used for equality comparison.
        /// </summary>
        protected abstract IEnumerable<object?> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType()) return false;
            var other = (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
        }

        public static bool operator ==(ValueObject? left, ValueObject? right) => Equals(left, right);
        public static bool operator !=(ValueObject? left, ValueObject? right) => !Equals(left, right);
    }
}
