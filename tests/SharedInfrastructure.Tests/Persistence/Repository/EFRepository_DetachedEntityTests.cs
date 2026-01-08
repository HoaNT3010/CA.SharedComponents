using FluentAssertions;
using SharedInfrastructure.Persistence;
using SharedInfrastructure.Tests.Stubs;
using SharedInfrastructure.Tests.Utilities;

namespace SharedInfrastructure.Tests.Persistence.Repository
{
    public class EFRepository_DetachedEntityTests
        : IClassFixture<SharedSqliteDbFixture>
    {
        private readonly SharedSqliteDbFixture _fixture;

        public EFRepository_DetachedEntityTests(SharedSqliteDbFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task BulkDelete_ShouldRemoveDetachedEntities()
        {
            var aggregates = new List<TestAggregate>();
            using (var setupContext = _fixture.Create())
            {
                // WORK ARROUND: Drop and create new database instance to clear leftover entities
                // from other test cases in the same fixture, since assertion check for all
                // entities in the dbset, not the just entities that were added in this test case
                setupContext.Database.EnsureDeleted();
                setupContext.Database.EnsureCreated();
                for (int i = 0; i < 2; i++)
                {
                    aggregates.Add(new TestAggregate { Id = Guid.NewGuid(), Name = $"Delete-{i + 1}" });
                }
                await setupContext.AddRangeAsync(aggregates);
                await setupContext.SaveChangesAsync();
            }

            using (var context = _fixture.Create())
            {
                var repo = new EFRepository<TestAggregate, Guid>(context);
                repo.BulkDelete(aggregates);
                await context.SaveChangesAsync();
            }

            using (var verifyContext = _fixture.Create())
            {
                verifyContext.Aggregates.Should().BeEmpty();
            }
        }

        [Fact]
        public async Task Update_ShouldAttachDetachedEntity()
        {
            TestAggregate aggregate;
            using (var setupContext = _fixture.Create())
            {
                aggregate = new TestAggregate
                {
                    Id = Guid.NewGuid(),
                    Name = "Before"
                };
                await setupContext.Aggregates.AddAsync(aggregate);
                await setupContext.SaveChangesAsync();
            }
            using var context = _fixture.Create();
            var repo = new EFRepository<TestAggregate, Guid>(context);

            aggregate.Name = "After";
            repo.Update(aggregate);
            await context.SaveChangesAsync();
            var result = await context.Aggregates.FindAsync(aggregate.Id);

            result.Should().NotBeNull();
            result.Name.Should().Be("After");
        }

        [Fact]
        public async Task Delete_ShouldRemoveDetachedEntity()
        {
            TestAggregate aggregate;
            using (var setupContext = _fixture.Create())
            {
                aggregate = new TestAggregate
                {
                    Id = Guid.NewGuid(),
                    Name = "Test"
                };
                await setupContext.Aggregates.AddAsync(aggregate);
                await setupContext.SaveChangesAsync();
            }
            using var context = _fixture.Create();
            var repo = new EFRepository<TestAggregate, Guid>(context);
            repo.Delete(aggregate);
            await context.SaveChangesAsync();
            var result = await context.Aggregates.FindAsync(aggregate.Id);

            result.Should().BeNull();
        }

        [Fact]
        public async Task BulkUpdate_ShouldUpdateAllDetachedEntities()
        {
            var aggregates = new List<TestAggregate>();
            using (var setupContext = _fixture.Create())
            {
                for (int i = 0; i < 2; i++)
                {
                    aggregates.Add(new TestAggregate { Id = Guid.NewGuid(), Name = $"Before-{i + 1}" });
                }
                await setupContext.AddRangeAsync(aggregates);
                await setupContext.SaveChangesAsync();
            }
            aggregates.ForEach(x => x.Name = "After");

            using (var context = _fixture.Create())
            {
                var repo = new EFRepository<TestAggregate, Guid>(context);
                repo.BulkUpdate(aggregates);
                await context.SaveChangesAsync();
            }

            using (var verifyContext = _fixture.Create())
            {
                var result = verifyContext.Aggregates.ToList();
                result.Should().HaveCount(2);
                result.Should().AllSatisfy(x => x.Name.Should().Be("After"));
            }
        }
    }
}
