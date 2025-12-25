namespace SharedDomain.Abstractions
{
    /// <summary>
    /// Defines the contract for an Aggregate Root, which acts as the primary entry point for a cluster of domain objects.
    /// </summary>
    /// <typeparam name="TKey">The type used for the aggregate root's primary key.</typeparam>
    /// <remarks>
    /// This interface follows the <b>strict architectural approach</b>, ensuring that all
    /// aggregate roots are identified, fully auditable, and capable of raising domain events.
    /// </remarks>
    public interface IAggregateRoot<TKey> : IEntity<TKey>, IHasDomainEvents;
}
