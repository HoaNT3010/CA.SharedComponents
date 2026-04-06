using MediatR;
using SharedApplication.Abstractions.Messaging;
using SharedDomain.Events;

namespace SharedApplication.Messaging
{
    /// <summary>
    /// Dispatches domain events to a MediatR <see cref="IPublisher"/> by wrapping them in
    /// <see cref="DomainEventNotificationAdapter{TEvent}"/> instances and publishing as <see cref="INotification"/>.
    /// </summary>
    /// <param name="publisher">The MediatR publisher used to publish notifications.</param>
    public class MediatRDomainEventDispatcher(IPublisher publisher) : IDomainEventDispatcher
    {
        /// <summary>
        /// Dispatches a single domain event by creating a notification adapter and publishing it.
        /// </summary>
        /// <param name="domainEvent">The domain event to dispatch. Must not be <c>null</c>.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous publish operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="domainEvent"/> is <c>null</c>.</exception>
        public async Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(domainEvent, nameof(domainEvent));
            var notification = CreateNotification(domainEvent);
            await publisher.Publish(notification, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Dispatches multiple domain events by creating notification adapters for each and publishing them sequentially.
        /// </summary>
        /// <param name="domainEvents">The collection of domain events to dispatch. Must not be <c>null</c> and must not contain <c>null</c> elements.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the tasks to complete.</param>
        /// <returns>A task that represents the asynchronous publish operations.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="domainEvents"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="domainEvents"/> contains a <c>null</c> element.</exception>
        public async Task DispatchMultipleAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(domainEvents, nameof(domainEvents));
            foreach (var domainEvent in domainEvents)
            {
                if (domainEvent is null)
                    throw new ArgumentException("Collection contains a null domain event.", nameof(domainEvents));
                var notification = CreateNotification(domainEvent);
                await publisher.Publish(notification, cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Creates an <see cref="INotification"/> instance that wraps the provided domain event.
        /// </summary>
        /// <param name="domainEvent">The domain event to wrap. Must not be <c>null</c>.</param>
        /// <returns>An <see cref="INotification"/> that wraps the provided domain event.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="domainEvent"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">Thrown when a matching notification instance cannot be created.</exception>
        private static INotification CreateNotification(IDomainEvent domainEvent)
        {
            ArgumentNullException.ThrowIfNull(domainEvent, nameof(domainEvent));
            var notificationType = typeof(DomainEventNotificationAdapter<>)
                .MakeGenericType(domainEvent.GetType());
            var notification = Activator.CreateInstance(notificationType, domainEvent);
            if (notification is not INotification typedNotification)
                throw new InvalidOperationException($"Failed to create notification for domain event type '{domainEvent.GetType().Name}'.");
            return typedNotification;
        }
    }
}
