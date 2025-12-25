using FluentAssertions;
using SharedDomain.Primitives;

namespace SharedDomain.Tests.Primitives
{
    public class EntityTests
    {
        private sealed class TestEntity : Entity<Guid> { }
        private sealed class AnotherTestEntity : Entity<Guid> { }
        private sealed class StringKeyEntity : Entity<string> { }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenObjectIsNotEntity()
        {
            // Arrange
            var entity = new TestEntity();

            // Assert
            entity.Equals(new object()).Should().BeFalse();
        }

        [Fact]
        public void Equals_ShouldReturnTrue_ForSameReference()
        {
            // Arrange
            var entity = new TestEntity();

            // Assert
            entity.Equals(entity).Should().BeTrue();
        }

        [Fact]
        public void Equals_ShouldReturnTrue_WhenSameTypesAndSameIds()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity1 = new TestEntity { Id = id };
            var entity2 = new TestEntity { Id = id };

            // Assert
            entity1.Equals(entity2).Should().BeTrue();
            (entity1 == entity2).Should().BeTrue();
            (entity1 != entity2).Should().BeFalse();
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenSameTypesButDifferentIds()
        {
            // Arrange
            var entity1 = new TestEntity { Id = Guid.NewGuid() };
            var entity2 = new TestEntity { Id = Guid.NewGuid() };

            // Assert
            entity1.Equals(entity2).Should().BeFalse();
            (entity1 != entity2).Should().BeTrue();
            (entity1 == entity2).Should().BeFalse();
        }

        [Fact]
        public void Equals_ShouldReturnFalse_ForDifferentTypesWithSameIds()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity1 = new TestEntity { Id = id };
            var entity2 = new AnotherTestEntity { Id = id };

            // Assert
            entity1.Equals(entity2).Should().BeFalse();
            (entity1 == entity2).Should().BeFalse();
            (entity1 != entity2).Should().BeTrue();
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenBothEntitiesAreTransient()
        {
            // Arrange
            var entity1 = new TestEntity();
            var entity2 = new TestEntity();

            // Assert
            entity1.Should().NotBe(entity2);
            entity1.Equals(entity2).Should().BeFalse();
            (entity1 == entity2).Should().BeFalse();
            (entity1 != entity2).Should().BeTrue();
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenEitherEntityIsTransient()
        {
            // Arrange
            var entity1 = new TestEntity();
            var entity2 = new TestEntity { Id = Guid.NewGuid() };

            // Assert
            entity1.Equals(entity2).Should().BeFalse();
            (entity1 == entity2).Should().BeFalse();
            (entity1 != entity2).Should().BeTrue();
        }

        [Fact]
        public void SoftDelete_ShouldMarkEntityAsDeleted()
        {
            // Arrange
            var deletedAt = DateTimeOffset.UtcNow;
            var deletedBy = "user";
            var entity = new TestEntity();

            // Act
            entity.SoftDelete(deletedAt, deletedBy);

            // Assert
            entity.IsDeleted.Should().BeTrue();
            entity.DeletedAt.Should().Be(deletedAt);
            entity.DeletedBy.Should().Be(deletedBy);
        }

        [Fact]
        public void SoftDelete_ShouldDoNothing_WhenEntityAlreadyDeleted()
        {
            // Arrange
            var deletedAt = DateTimeOffset.UtcNow;
            var deletedBy = "user";
            var entity = new TestEntity();

            // Act
            entity.SoftDelete(deletedAt, deletedBy);
            entity.SoftDelete(deletedAt.AddDays(1), "admin");

            // Assert
            entity.DeletedAt.Should().Be(deletedAt);
            entity.DeletedBy.Should().Be(deletedBy);
        }

        [Fact]
        public void Restore_ShouldClearDeletionState()
        {
            // Arrange
            var entity = new TestEntity();
            entity.SoftDelete();

            // Act
            entity.Restore();

            // Assert
            entity.IsDeleted.Should().BeFalse();
            entity.DeletedAt.Should().BeNull();
            entity.DeletedBy.Should().BeNull();
        }

        [Fact]
        public void Restore_ShouldDoNothing_WhenNotDeleted()
        {
            // Arrange
            var entity = new TestEntity();

            // Act
            entity.Restore();

            // Assert
            entity.IsDeleted.Should().BeFalse();
            entity.DeletedAt.Should().BeNull();
            entity.DeletedBy.Should().BeNull();
        }

        [Fact]
        public void Equals_ShouldReturnFalse_WhenEitherIdIsNull()
        {
            // Arrange
            var entity1 = new StringKeyEntity { Id = null! };
            var entity2 = new StringKeyEntity { Id = "id" };

            // Assert
            entity1.Equals(entity2).Should().BeFalse();
            (entity1 != entity2).Should().BeTrue();
            (entity1 == entity2).Should().BeFalse();
        }

        [Fact]
        public void GetHashCode_ShouldBeEqual_ForEntitiesWithSameId()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity1 = new TestEntity { Id = id };
            var entity2 = new TestEntity { Id = id };

            // Assert
            entity1.GetHashCode().Should().Be(entity2.GetHashCode());
        }
    }
}
