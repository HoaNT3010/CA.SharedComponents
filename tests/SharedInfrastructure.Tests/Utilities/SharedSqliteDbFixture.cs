using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SharedInfrastructure.Tests.Stubs;

namespace SharedInfrastructure.Tests.Utilities
{
    public sealed class SharedSqliteDbFixture : IDisposable
    {
        const string ConnectionString = "Filename=:memory:";
        private readonly SqliteConnection _connection;

        public SharedSqliteDbFixture()
        {
            _connection = new SqliteConnection(ConnectionString);
            _connection.Open();
        }

        public TestDbContext Create()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseSqlite(_connection)
            .Options;
            var context = new TestDbContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
