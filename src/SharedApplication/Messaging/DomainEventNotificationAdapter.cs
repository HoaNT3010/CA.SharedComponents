using MediatR;
using SharedDomain.Events;

namespace SharedApplication.Messaging
{
    /// <summary>
    /// Wraps a domain event so it can be published through MediatR as an <see cref="INotification"/>.
    /// </summary>
    /// <typeparam name="TEvent">The concrete domain event type.</typeparam>
    public sealed class DomainEventNotificationAdapter<TEvent>
        : INotification
        where TEvent : IDomainEvent
    {
        /// <summary>
        /// Gets the wrapped domain event instance.
        /// </summary>
        public TEvent DomainEvent { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEventNotificationAdapter{TEvent}"/> class.
        /// </summary>
        /// <param name="domainEvent">The domain event to wrap. Must not be <c>null</c>.</param>
        public DomainEventNotificationAdapter(TEvent domainEvent)
        {
            DomainEvent = domainEvent ?? throw new ArgumentNullException(nameof(domainEvent));
        }

        /// <summary>
        /// Creates a new adapter instance for the specified domain event.
        /// </summary>
        /// <param name="domainEvent">The domain event to wrap.</param>
        /// <returns>A <see cref="DomainEventNotificationAdapter{TEvent}"/> that wraps the provided event.</returns>
        public static DomainEventNotificationAdapter<TEvent> Create(TEvent domainEvent) => new(domainEvent);
    }
}
