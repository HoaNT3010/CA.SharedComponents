using SharedDomain.Events;

namespace SharedDomain.Abstractions
{
    /// <summary>
    /// Defines a contract for entities that can generate and store domain events.
    /// </summary>
    /// <remarks>
    /// Domain events represent significant occurrences within the domain that other parts 
    /// of the system may need to react to. These events are typically dispatched 
    /// just before or after the entity is persisted to the database.
    /// </remarks>
    public interface IHasDomainEvents
    {
        /// <summary>
        /// Gets a read-only collection of domain events raised by this entity.
        /// </summary>
        /// <value>
        /// A collection of <see cref="IDomainEvent"/> objects.
        /// </value>
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    }
}
