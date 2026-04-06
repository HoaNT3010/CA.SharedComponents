using FluentAssertions;
using MediatR;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using SharedApplication.Messaging;
using SharedApplication.Tests.Stubs;
using SharedDomain.Events;

namespace SharedApplication.Tests.Messaging
{
    public class MediatRDomainEventDispatcherTests
    {
        readonly IPublisher _publisher;
        readonly MediatRDomainEventDispatcher _dispatcher;

        public MediatRDomainEventDispatcherTests()
        {
            _publisher = Substitute.For<IPublisher>();
            _dispatcher = new MediatRDomainEventDispatcher(_publisher);
        }

        private static bool CheckAdapter(INotification notification, TestDomainEvent expectedEvent)
        {
            return notification is DomainEventNotificationAdapter<TestDomainEvent> adapter &&
                adapter.DomainEvent == expectedEvent;
        }

        [Fact]
        public async Task DispatchAsync_ShouldPublishEvent()
        {
            var domainEvent = new TestDomainEvent();

            await _dispatcher.DispatchAsync(domainEvent);

            await _publisher.Received(1).Publish(
                Arg.Is<INotification>(n => CheckAdapter(n, domainEvent)),
                Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task DispatchAsync_ShouldThrow_WhenDomainEventIsNull()
        {
            var act = async () => await _dispatcher.DispatchAsync(null!);

            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task DispatchMultipleAsync_ShouldPublishAllEvents()
        {
            var events = new IDomainEvent[]
            {
                new TestDomainEvent(),
                new TestDomainEvent()
            };

            await _dispatcher.DispatchMultipleAsync(events);

            await _publisher.Received(2).Publish(
                Arg.Any<INotification>(),
                Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task DispatchMultipleAsync_ShouldWrapEachEventCorrectly()
        {
            var event1 = new TestDomainEvent();
            var event2 = new TestDomainEvent();
            var events = new IDomainEvent[] { event1, event2 };

            await _dispatcher.DispatchMultipleAsync(events);

            await _publisher.Received(1).Publish(
                Arg.Is<INotification>(n => CheckAdapter(n, event1)),
                Arg.Any<CancellationToken>());
            await _publisher.Received(1).Publish(
                Arg.Is<INotification>(n => CheckAdapter(n, event2)),
                Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task DispatchMultipleAsync_ShouldThrow_WhenCollectionIsNull()
        {
            var act = async () => await _dispatcher.DispatchMultipleAsync(null!);

            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task DispatchMultipleAsync_ShouldThrow_WhenCollectionContainsNull()
        {
            var events = new IDomainEvent[]
            {
                new TestDomainEvent(),
                null!
            };

            var act = async () => await _dispatcher.DispatchMultipleAsync(events);

            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task DispatchMultipleAsync_ShouldPublishInOrder()
        {
            var event1 = new TestDomainEvent();
            var event2 = new TestDomainEvent();
            var events = new IDomainEvent[] { event1, event2 };

            await _dispatcher.DispatchMultipleAsync(events);

            Received.InOrder(async () =>
            {
                await _publisher.Publish(
                    Arg.Is<INotification>(n => CheckAdapter(n, event1)),
                    Arg.Any<CancellationToken>());
                await _publisher.Publish(
                    Arg.Is<INotification>(n => CheckAdapter(n, event2)),
                    Arg.Any<CancellationToken>());
            });
        }
    }
}
