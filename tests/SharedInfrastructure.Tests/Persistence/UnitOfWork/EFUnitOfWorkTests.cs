using SharedInfrastructure.Persistence;
using SharedInfrastructure.Tests.Utilities;
using SharedInfrastructure.Tests.Stubs;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace SharedInfrastructure.Tests.Persistence.UnitOfWork
{
    public class EFUnitOfWorkTests
        : IClassFixture<SharedSqliteDbFixture>
    {
        readonly SharedSqliteDbFixture _fixture;

        public EFUnitOfWorkTests(SharedSqliteDbFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task SaveChangesAsync_ShouldPersistsChanges()
        {
            using var context = _fixture.Create();
            var uow = new EFUnitOfWork(context);
            context.Aggregates.Add(new TestAggregate { Id = Guid.NewGuid(), Name = "Test" });

            var result = await uow.SaveChangesAsync();

            result.Should().Be(1);
        }

        [Fact]
        public async Task BeginTransactionAsync_ShouldSetsActiveTransaction()
        {
            using var context = _fixture.Create();
            var uow = new EFUnitOfWork(context);

            await uow.BeginTransactionAsync();

            uow.HasActiveTransaction.Should().BeTrue();
        }

        [Fact]
        public async Task BeginTransactionAsync_ShouldThrowsException_WhenATransactionIsActive()
        {
            using var context = _fixture.Create();
            var uow = new EFUnitOfWork(context);
            await uow.BeginTransactionAsync();

            var act = () => uow.BeginTransactionAsync();

            await act.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task CommitTransactionAsync_ShouldCommitsChanges_AndClearsTransaction()
        {
            using var context = _fixture.Create();
            var uow = new EFUnitOfWork(context);
            await uow.BeginTransactionAsync();
            context.Aggregates.Add(new TestAggregate { Id = Guid.NewGuid(), Name = "Test" });
            await uow.SaveChangesAsync();

            await uow.CommitTransactionAsync();

            uow.HasActiveTransaction.Should().BeFalse();
            using var verifyContext = _fixture.Create();
            (await verifyContext.Aggregates.CountAsync()).Should().Be(1);
        }

        [Fact]
        public async Task CommitTransactionAsync_ShouldThrowsException_WithNoActiveTransaction()
        {
            using var context = _fixture.Create();
            var uow = new EFUnitOfWork(context);

            var act = () => uow.CommitTransactionAsync();

            await act.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task RollbackTransactionAsync_ShouldDiscardsChanges_AndClearsTransaction()
        {
            using var context = _fixture.Create();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var uow = new EFUnitOfWork(context);
            await uow.BeginTransactionAsync();
            context.Aggregates.Add(new TestAggregate { Id = Guid.NewGuid(), Name = "Test" });
            await uow.SaveChangesAsync();

            await uow.RollbackTransactionAsync();

            uow.HasActiveTransaction.Should().BeFalse();
            using var verifyContext = _fixture.Create();
            (await verifyContext.Aggregates.CountAsync()).Should().Be(0);
        }

        [Fact]
        public async Task RollbackTransactionAsync_ShouldThrowsException_WithNoActiveTransaction()
        {
            using var context = _fixture.Create();
            var uow = new EFUnitOfWork(context);

            var act = () => uow.RollbackTransactionAsync();

            await act.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task RollbackTransactionAsync_ShouldDiscardsChanges_WhenSavingChangesMultipleTimes()
        {
            using var context = _fixture.Create();
            var uow = new EFUnitOfWork(context);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            await uow.BeginTransactionAsync();
            context.Aggregates.Add(new TestAggregate { Id = Guid.NewGuid(), Name = "Test 1" });
            await uow.SaveChangesAsync();
            context.Aggregates.Add(new TestAggregate { Id = Guid.NewGuid(), Name = "Test 2" });
            await context.SaveChangesAsync();

            await uow.RollbackTransactionAsync();

            using var verifyContext = _fixture.Create();
            (await verifyContext.Aggregates.CountAsync()).Should().Be(0);
        }
    }
}
