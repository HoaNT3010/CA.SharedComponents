using Microsoft.EntityFrameworkCore;

namespace SharedInfrastructure.Tests.Stubs
{
    public sealed class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }

        public DbSet<TestAggregate> Aggregates => Set<TestAggregate>();
    }
}
