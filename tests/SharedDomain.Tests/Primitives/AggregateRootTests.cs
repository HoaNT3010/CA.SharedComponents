using FluentAssertions;
using SharedDomain.Events;
using SharedDomain.Primitives;

namespace SharedDomain.Tests.Primitives
{
    public class AggregateRootTests
    {
        private sealed class TestAggregate : AggregateRoot<Guid>
        {
            public void Raise(IDomainEvent domainEvent) => AddDomainEvent(domainEvent);
        }
        private sealed class TestDomainEvent : DomainEvent;

        [Fact]
        public void AddDomainEvent_ShouldAddEventToCollection()
        {
            // Arrange
            var aggregate = new TestAggregate();
            var domainEvent = new TestDomainEvent();

            // Act
            aggregate.Raise(domainEvent);

            // Assert
            aggregate.DomainEvents.Should().HaveCount(1);
            aggregate.DomainEvents.Should()
                .ContainSingle()
                .Which.Should().Be(domainEvent);
        }

        [Fact]
        public void DomainEvents_ShouldBeReadonly()
        {
            // Arrange
            var aggregate = new TestAggregate();

            // Assert
            aggregate.DomainEvents.Should().BeAssignableTo<IReadOnlyCollection<IDomainEvent>>();
        }

        [Fact]
        public void DomainEvents_ShouldNotAllowModification()
        {
            // Arrange
            var aggregate = new TestAggregate();

            // Act
            var act = () => ((ICollection<IDomainEvent>)aggregate.DomainEvents).Add(new TestDomainEvent());

            // Assert
            act.Should().Throw<NotSupportedException>();
        }

        [Fact]
        public void ClearDomainEvents_ShouldRemoveAllEvents()
        {
            // Arrange
            var aggregate = new TestAggregate();
            aggregate.Raise(new TestDomainEvent());
            aggregate.Raise(new TestDomainEvent());

            // Act
            aggregate.ClearDomainEvents();

            // Assert
            aggregate.DomainEvents.Should().BeEmpty();
        }

        [Fact]
        public void ClearDomainEvents_ShouldNotThrow_WhenCalledMultipleTimes()
        {
            // Arrange
            var aggregate = new TestAggregate();
            aggregate.Raise(new TestDomainEvent());

            // Act
            aggregate.ClearDomainEvents();
            aggregate.ClearDomainEvents();

            // Assert
            aggregate.DomainEvents.Should().BeEmpty();
        }

        [Fact]
        public void ClearDomainEvents_ShouldNotAffectEntityState()
        {
            // Arrange
            var id = Guid.NewGuid();
            var aggregate = new TestAggregate { Id = id };
            aggregate.Raise(new TestDomainEvent());

            // Act
            aggregate.ClearDomainEvents();

            // Assert
            aggregate.Id.Should().NotBe(Guid.Empty);
            aggregate.Id.Should().Be(id);
        }

        [Fact]
        public void AddDomainEvent_ShouldMaintainOrder_WhenAddMultipleEvents()
        {
            // Arrange
            var aggregate = new TestAggregate();
            var event1 = new TestDomainEvent();
            var event2 = new TestDomainEvent();

            // Act
            aggregate.Raise(event1);
            aggregate.Raise(event2);

            // Assert
            aggregate.DomainEvents.Should().HaveCount(2);
            aggregate.DomainEvents.Should().ContainInOrder(event1, event2);
        }
    }
}
