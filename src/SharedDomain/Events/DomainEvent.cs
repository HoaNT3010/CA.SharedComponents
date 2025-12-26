namespace SharedDomain.Events
{
    /// <summary>
    /// Represents the base type for domain events within the application.
    /// </summary>
    /// <remarks>Domain events are used to signal significant occurrences or state changes within the domain
    /// model. Implementations should derive from this class to define specific event types. Each event instance is
    /// assigned a unique identifier and timestamp at creation.</remarks>
    public abstract class DomainEvent : IDomainEvent
    {
        /// <summary>
        /// Gets the unique identifier for this instance.
        /// </summary>
        public Guid Id { get; } = Guid.NewGuid();
        /// <summary>
        /// Gets the date and time, in Coordinated Universal Time (UTC), when the event occurred.
        /// </summary>
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
