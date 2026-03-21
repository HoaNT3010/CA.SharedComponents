using SharedDomain.Events;

namespace SharedApplication.Abstractions.Messaging
{
    /// <summary>
    /// Responsible for dispatching domain events to the appropriate handlers or infrastructure.
    /// </summary>
    public interface IDomainEventDispatcher
    {
        /// <summary>
        /// Dispatches a single domain event.
        /// </summary>
        /// <param name="domainEvent">The domain event to dispatch.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous dispatch operation.</returns>
        Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);

        /// <summary>
        /// Dispatches multiple domain events.
        /// </summary>
        /// <param name="domainEvents">The collection of domain events to dispatch.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous dispatch operations.</returns>
        Task DispatchMultipleAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
    }
}
