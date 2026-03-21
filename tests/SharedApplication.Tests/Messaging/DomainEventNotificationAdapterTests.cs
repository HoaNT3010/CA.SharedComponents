using FluentAssertions;
using SharedApplication.Messaging;
using SharedApplication.Tests.Stubs;

namespace SharedApplication.Tests.Messaging
{
    public class DomainEventNotificationAdapterTests
    {
        [Fact]
        public void Create_ShouldWrapDomainEvent()
        {
            var domainEvent = new TestDomainEvent();

            var adapter = DomainEventNotificationAdapter<TestDomainEvent>.Create(domainEvent);

            adapter.DomainEvent.Should().BeSameAs(domainEvent);
        }

        [Fact]
        public void Create_ShouldThrow_WhenDomainEventIsNull()
        {
            var act = () => DomainEventNotificationAdapter<TestDomainEvent>.Create(null!);

            act.Should().Throw<ArgumentNullException>();
        }
    }
}
