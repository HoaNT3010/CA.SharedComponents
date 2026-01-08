using SharedDomain.Abstractions;

namespace SharedApplication.Persistence
{
    /// <summary>
    /// Defines write operations for an aggregate root repository.
    /// This abstraction covers single-entity insert, update and delete operations.
    /// </summary>
    /// <typeparam name="TAggregate">Aggregate root type.</typeparam>
    /// <typeparam name="TKey">Aggregate key type.</typeparam>
    public interface IWriteRepository<TAggregate, TKey>
        where TAggregate : IAggregateRoot<TKey>
    {
        /// <summary>
        /// Asynchronously adds the specified aggregate to the repository.
        /// Implementations should persist the aggregate or schedule it for persistence.
        /// </summary>
        /// <param name="aggregate">The aggregate to add.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        Task AddAsync(TAggregate aggregate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the specified aggregate in the repository.
        /// Implementation may attach the aggregate to a change tracker and mark it as modified.
        /// </summary>
        /// <param name="aggregate">The aggregate to update.</param>
        void Update(TAggregate aggregate);

        /// <summary>
        /// Deletes the specified aggregate from the repository.
        /// Implementation may mark the aggregate as removed or remove it from the underlying store.
        /// </summary>
        /// <param name="aggregate">The aggregate to delete.</param>
        void Delete(TAggregate aggregate);
    }
}
