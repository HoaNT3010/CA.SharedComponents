using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SharedInfrastructure.Tests.Stubs;

namespace SharedInfrastructure.Tests.Utilities
{
    internal static class IsolatedDbContextFactory
    {
        const string ConnectionString = "Filename=:memory:";

        public static TestDbContext Create()
        {
            var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseSqlite(connection)
                .Options;
            var context = new TestDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
