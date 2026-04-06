using SharedDomain.Abstractions;

namespace SharedApplication.Abstractions.Persistence
{
    /// <summary>
    /// Represents a repository that aggregates all write operations for an aggregate root.
    /// Combines single-entity write operations (add, update, delete) and bulk write operations.
    /// </summary>
    /// <typeparam name="TAggregate">The aggregate root type.</typeparam>
    /// <typeparam name="TKey">The aggregate key type.</typeparam>
    public interface IFullWriteRepository<TAggregate, TKey>
        : IWriteRepository<TAggregate, TKey>,
          IBulkWriteRepository<TAggregate, TKey>
        where TAggregate : IAggregateRoot<TKey>;
}
