using SharedDomain.Abstractions;
using SharedDomain.Events;

namespace SharedDomain.Primitives
{
    /// <summary>
    /// Provides a base implementation for Aggregate Roots, extending the standard
    /// entity with domain event management.
    /// </summary>
    /// <typeparam name="TKey">The type of the unique identifier.</typeparam>
    /// <remarks>
    /// This implementation follows the <b>strict approach</b>, combining entity identity,
    /// full audit metadata, and domain event dispatching capabilities into a single inheritance point.
    /// </remarks>
    public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<TKey>
    {
        private readonly List<IDomainEvent> _domainEvents = [];
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        /// <summary>
        /// Registers a new domain event to be dispatched.
        /// </summary>
        /// <param name="domainEvent">The event to add.</param>
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        /// <inheritdoc/>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
