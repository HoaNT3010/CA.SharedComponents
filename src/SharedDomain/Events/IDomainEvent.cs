namespace SharedDomain.Events
{
    /// <summary>
    /// Represents a domain event that captures a significant occurrence within the business domain.
    /// </summary>
    /// <remarks>Domain events are used to signal that something of interest has happened in the system,
    /// typically for the purposes of event sourcing, auditing, or triggering side effects. Implementations should
    /// ensure that each event instance is uniquely identifiable and records the time at which it occurred.</remarks>
    public interface IDomainEvent
    {
        /// <summary>
        /// Gets the unique identifier for the instance.
        /// </summary>
        Guid Id { get; }
        /// <summary>
        /// Gets the date and time at which the event occurred.
        /// </summary>
        DateTime OccurredOn { get; }
    }
}
