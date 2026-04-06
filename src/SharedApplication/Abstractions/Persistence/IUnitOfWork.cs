namespace SharedApplication.Abstractions.Persistence
{
    /// <summary>
    /// Coordinates saving changes and transaction management for a set of repositories.
    /// Implementations should ensure transactional consistency across repository operations.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets a value that indicates whether there is an active transaction associated with the current unit of work.
        /// </summary>
        /// <value>
        /// <c>true</c> when a transaction has been started and has not yet been committed or rolled back; otherwise, <c>false</c>.
        /// </value>
        bool HasActiveTransaction { get; }
        /// <summary>
        /// Persists all changes made in the current unit of work to the underlying data store.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the operation to complete.</param>
        /// <returns>
        /// A <see cref="Task{Int32}"/> that completes once changes are saved. The result contains
        /// the number of state entries written to the underlying database.
        /// </returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Begins a new transaction scope for the unit of work.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the operation to complete.</param>
        /// <returns>A <see cref="Task"/> that completes when the transaction has been started.</returns>
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Commits the active transaction associated with the unit of work.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the operation to complete.</param>
        /// <returns>A <see cref="Task"/> that completes when the transaction has been committed.</returns>
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Rolls back the active transaction associated with the unit of work.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the operation to complete.</param>
        /// <returns>A <see cref="Task"/> that completes when the transaction has been rolled back.</returns>
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}
