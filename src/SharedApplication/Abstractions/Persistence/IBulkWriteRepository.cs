using SharedDomain.Abstractions;

namespace SharedApplication.Abstractions.Persistence
{
    /// <summary>
    /// Defines bulk write operations for an aggregate root repository.
    /// Use these operations to perform multi-entity inserts, updates or deletes in a single call.
    /// </summary>
    /// <typeparam name="TAggregate">Aggregate root type.</typeparam>
    /// <typeparam name="TKey">Aggregate key type.</typeparam>
    public interface IBulkWriteRepository<TAggregate, TKey>
        where TAggregate : IAggregateRoot<TKey>
    {
        /// <summary>
        /// Asynchronously inserts multiple aggregates in bulk.
        /// </summary>
        /// <param name="aggregates">The aggregates to insert.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        Task BulkInsertAsync(IEnumerable<TAggregate> aggregates, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates multiple aggregates in bulk.
        /// Implementations may optimize for batch updates when supported by the underlying store.
        /// </summary>
        /// <param name="aggregates">The aggregates to update.</param>
        void BulkUpdate(IEnumerable<TAggregate> aggregates);

        /// <summary>
        /// Deletes multiple aggregates in bulk.
        /// Implementations may optimize for batch deletes when supported by the underlying store.
        /// </summary>
        /// <param name="aggregates">The aggregates to delete.</param>
        void BulkDelete(IEnumerable<TAggregate> aggregates);
    }
}
