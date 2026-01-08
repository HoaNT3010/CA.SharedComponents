using FluentAssertions;
using SharedInfrastructure.Persistence;
using SharedInfrastructure.Tests.Stubs;
using SharedInfrastructure.Tests.Utilities;

namespace SharedInfrastructure.Tests.Persistence.Repository
{
    public class EFRepositoryTests
    {
        [Fact]
        public async Task AddAsync_ShouldPersistEntity_WhenSaveChangesIsCalled()
        {
            using var context = IsolatedDbContextFactory.Create();
            var repo = new EFRepository<TestAggregate, Guid>(context);
            var aggregate = new TestAggregate
            {
                Id = Guid.NewGuid(),
                Name = "Test"
            };

            await repo.AddAsync(aggregate);
            await context.SaveChangesAsync();
            var result = await context.Aggregates.FindAsync(aggregate.Id);

            result.Should().NotBeNull();
            result.Id.Should().Be(aggregate.Id);
            result.Name.Should().Be("Test");
        }

        [Fact]
        public async Task AddAsync_ShouldMarkEntityAsAdded()
        {
            using var context = IsolatedDbContextFactory.Create();
            var repo = new EFRepository<TestAggregate, Guid>(context);
            var aggregate = new TestAggregate
            {
                Id = Guid.NewGuid(),
                Name = "Test"
            };
            await repo.AddAsync(aggregate);

            context.Entry(aggregate).State.Should().Be(Microsoft.EntityFrameworkCore.EntityState.Added);
        }

        [Fact]
        public async Task AnyAsync_ShouldReturnTrue_WhenMatchExists()
        {
            using var context = IsolatedDbContextFactory.Create();
            var repo = new EFRepository<TestAggregate, Guid>(context);
            await repo.AddAsync(new TestAggregate
            {
                Id = Guid.NewGuid(),
                Name = "Exists"
            });
            await context.SaveChangesAsync();
            var spec = new TestByNameSpec("Exists");

            var exists = await repo.AnyAsync(spec);

            exists.Should().BeTrue();
        }

        [Fact]
        public async Task AnyAsync_ShouldReturnFalse_WhenMatchNotExists()
        {
            using var context = IsolatedDbContextFactory.Create();
            var repo = new EFRepository<TestAggregate, Guid>(context);
            await repo.AddAsync(new TestAggregate
            {
                Id = Guid.NewGuid(),
                Name = "NotExists"
            });
            await context.SaveChangesAsync();
            var spec = new TestByNameSpec("Exists");

            var exists = await repo.AnyAsync(spec);

            exists.Should().BeFalse();
        }

        [Fact]
        public async Task CountAsync_ShouldReturnCorrectCount_WhenMatch()
        {
            using var context = IsolatedDbContextFactory.Create();
            var repo = new EFRepository<TestAggregate, Guid>(context);
            await repo.BulkInsertAsync([
                new TestAggregate{
                    Id = Guid.NewGuid(),
                    Name = "Test"
                },
                new TestAggregate{
                    Id = Guid.NewGuid(),
                    Name = "Test"
                }]);
            await context.SaveChangesAsync();
            var spec = new TestByNameSpec("Test");

            var result = await repo.CountAsync(spec);

            result.Should().Be(2);
        }

        [Fact]
        public async Task ListAsync_ShouldApplySpecification()
        {
            using var context = IsolatedDbContextFactory.Create();
            var repo = new EFRepository<TestAggregate, Guid>(context);
            await repo.BulkInsertAsync([
                new TestAggregate{
                    Id = Guid.NewGuid(),
                    Name = "A"
                },
                new TestAggregate{
                    Id = Guid.NewGuid(),
                    Name = "B"
                }]);
            await context.SaveChangesAsync();
            var spec = new TestByNameSpec("A");

            var result = await repo.ListAsync(spec);

            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(1);
            result.Single().Name.Should().Be("A");
        }

        [Fact]
        public async Task ListAsync_ShouldReturnEmptyCollection_WhenNoMatchExists()
        {
            using var context = IsolatedDbContextFactory.Create();
            var repo = new EFRepository<TestAggregate, Guid>(context);
            var spec = new TestByNameSpec("NotExists");

            var result = await repo.ListAsync(spec);

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task Delete_ShouldRemoveEntity_WhenSaveChangesIsCalled()
        {
            using var context = IsolatedDbContextFactory.Create();
            var repo = new EFRepository<TestAggregate, Guid>(context);
            var aggregate = new TestAggregate
            {
                Id = Guid.NewGuid(),
                Name = "Test"
            };
            await repo.AddAsync(aggregate);
            await context.SaveChangesAsync();

            repo.Delete(aggregate);
            await context.SaveChangesAsync();
            var result = await context.Aggregates.FindAsync(aggregate.Id);

            result.Should().BeNull();
        }

        [Fact]
        public async Task Delete_ShouldMarkEntityAsDeleted()
        {
            using var context = IsolatedDbContextFactory.Create();
            var repo = new EFRepository<TestAggregate, Guid>(context);
            var aggregate = new TestAggregate
            {
                Id = Guid.NewGuid(),
                Name = "Test"
            };
            await repo.AddAsync(aggregate);
            await context.SaveChangesAsync();

            repo.Delete(aggregate);

            context.Entry(aggregate).State.Should().Be(Microsoft.EntityFrameworkCore.EntityState.Deleted);
        }

        [Fact]
        public async Task Update_ShouldPersistChanges_WhenSaveChangesIsCalled()
        {
            using var context = IsolatedDbContextFactory.Create();
            var repo = new EFRepository<TestAggregate, Guid>(context);
            var aggregate = new TestAggregate
            {
                Id = Guid.NewGuid(),
                Name = "Before"
            };
            await repo.AddAsync(aggregate);
            await context.SaveChangesAsync();

            aggregate.Name = "After";
            repo.Update(aggregate);
            await context.SaveChangesAsync();
            var result = await context.Aggregates.FindAsync(aggregate.Id);

            result.Should().NotBeNull();
            result.Name.Should().Be("After");
        }

        [Fact]
        public async Task Update_ShouldMarkEntityAsModified()
        {
            using var context = IsolatedDbContextFactory.Create();
            var repo = new EFRepository<TestAggregate, Guid>(context);
            var aggregate = new TestAggregate
            {
                Id = Guid.NewGuid(),
                Name = "Test"
            };
            await repo.AddAsync(aggregate);
            await context.SaveChangesAsync();

            repo.Update(aggregate);

            context.Entry(aggregate).State.Should().Be(Microsoft.EntityFrameworkCore.EntityState.Modified);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectEntity_WhenIdMatch()
        {
            using var context = IsolatedDbContextFactory.Create();
            var repo = new EFRepository<TestAggregate, Guid>(context);
            var aggregate = new TestAggregate
            {
                Id = Guid.NewGuid(),
                Name = "Test"
            };
            await repo.AddAsync(aggregate);
            await context.SaveChangesAsync();

            var result = await repo.GetByIdAsync(aggregate.Id);

            result.Should().NotBeNull();
            result.Id.Should().Be(aggregate.Id);
            result.Name.Should().Be("Test");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNoEntityExistsWithId()
        {
            using var context = IsolatedDbContextFactory.Create();
            var repo = new EFRepository<TestAggregate, Guid>(context);
            var id = Guid.NewGuid();

            var result = await repo.GetByIdAsync(id);

            result.Should().BeNull();
        }

        [Fact]
        public async Task FirstOrDefaultAsync_ShouldReturnEntity_WhenMatchExists()
        {
            using var context = IsolatedDbContextFactory.Create();
            var repo = new EFRepository<TestAggregate, Guid>(context);
            var aggregate = new TestAggregate
            {
                Id = Guid.NewGuid(),
                Name = "Exists"
            };
            await repo.AddAsync(aggregate);
            await context.SaveChangesAsync();
            var spec = new TestByNameSpec("Exists");

            var result = await repo.FirstOrDefaultAsync(spec);

            result.Should().NotBeNull();
            result.Id.Should().Be(aggregate.Id);
            result.Name.Should().Be(aggregate.Name);
        }

        [Fact]
        public async Task FirstOrDefaultAsync_ShouldReturnNull_WhenNoMatchExists()
        {
            using var context = IsolatedDbContextFactory.Create();
            var repo = new EFRepository<TestAggregate, Guid>(context);
            var spec = new TestByNameSpec("NotExists");

            var result = await repo.FirstOrDefaultAsync(spec);

            result.Should().BeNull();
        }

        [Fact]
        public async Task BulkInsertAsync_ShouldPersistAllEntities_WhenSaveChangesIsCalled()
        {
            using var context = IsolatedDbContextFactory.Create();
            var repo = new EFRepository<TestAggregate, Guid>(context);

            await repo.BulkInsertAsync([
                new TestAggregate{ Id = Guid.NewGuid(), Name = "A" },
                new TestAggregate{ Id = Guid.NewGuid(), Name = "B" },
                new TestAggregate{ Id = Guid.NewGuid(), Name = "C" }]);
            await context.SaveChangesAsync();

            context.Aggregates.Count().Should().Be(3);
        }

        [Fact]
        public async Task BulkInsertAsync_ShouldNotPersistEntities_WithoutCallingSaveChanges()
        {
            using var context = IsolatedDbContextFactory.Create();
            var repo = new EFRepository<TestAggregate, Guid>(context);

            await repo.BulkInsertAsync([
                new TestAggregate{ Id = Guid.NewGuid(), Name = "A" },
                new TestAggregate{ Id = Guid.NewGuid(), Name = "B" }]);

            context.Aggregates.Count().Should().Be(0);
            context.ChangeTracker.Entries<TestAggregate>()
                .All(e => e.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                .Should().BeTrue();
        }

        [Fact]
        public async Task BulkDelete_ShouldRemoveAllEntities_WhenSaveChangesIsCalled()
        {
            var aggregates = new List<TestAggregate>();
            for (int i = 0; i < 2; i++)
            {
                aggregates.Add(new TestAggregate { Id = Guid.NewGuid(), Name = $"Test {i + 1}" });
            }
            using var context = IsolatedDbContextFactory.Create();
            var repo = new EFRepository<TestAggregate, Guid>(context);
            await repo.BulkInsertAsync(aggregates);
            await context.SaveChangesAsync();

            repo.BulkDelete(aggregates);
            await context.SaveChangesAsync();

            context.Aggregates.Count().Should().Be(0);
        }

        [Fact]
        public async Task BulkUpdate_ShouldUpdateAllEntities_WhenSaveChangesIsCalled()
        {
            var aggregates = new List<TestAggregate>();
            for (int i = 0; i < 2; i++)
            {
                aggregates.Add(new TestAggregate { Id = Guid.NewGuid(), Name = "Before" });
            }
            using var context = IsolatedDbContextFactory.Create();
            var repo = new EFRepository<TestAggregate, Guid>(context);
            await repo.BulkInsertAsync(aggregates);
            await context.SaveChangesAsync();
            foreach (var aggregate in aggregates)
            {
                aggregate.Name = "After";
            }

            repo.BulkUpdate(aggregates);
            await context.SaveChangesAsync();
            var result = context.Aggregates.ToList();

            result.Should().HaveCount(2);
            result.Should().AllSatisfy(x => x.Name.Should().Be("After"));
        }
    }
}
