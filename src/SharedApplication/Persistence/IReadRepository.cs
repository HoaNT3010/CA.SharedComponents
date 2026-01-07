using Ardalis.Specification;
using SharedDomain.Abstractions;

namespace SharedApplication.Persistence
{
    /// <summary>
    /// Defines read/query operations for an aggregate root repository.
    /// Use specifications to express query filters, projections and includes.
    /// </summary>
    /// <typeparam name="TAggregate">Aggregate root type.</typeparam>
    /// <typeparam name="TKey">Aggregate key type.</typeparam>
    public interface IReadRepository<TAggregate, TKey>
        where TAggregate : IAggregateRoot<TKey>
    {
        /// <summary>
        /// Gets an aggregate by its identifier.
        /// </summary>
        /// <param name="id">The aggregate identifier.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the aggregate
        /// if found; otherwise <c>null</c>.
        /// </returns>
        Task<TAggregate?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns the first aggregate that satisfies the provided specification, or <c>null</c> if none found.
        /// </summary>
        /// <param name="specification">Specification that defines the query criteria.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the first matching aggregate
        /// or <c>null</c> when no match exists.
        /// </returns>
        Task<TAggregate?> FirstOrDefaultAsync(ISpecification<TAggregate> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Lists aggregates that satisfy the provided specification.
        /// </summary>
        /// <param name="specification">Specification that defines the query criteria.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a read-only list
        /// of aggregates that match the specification.
        /// </returns>
        Task<IReadOnlyList<TAggregate>> ListAsync(ISpecification<TAggregate> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Counts aggregates that satisfy the provided specification.
        /// </summary>
        /// <param name="specification">Specification that defines the query criteria.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the number
        /// of aggregates that match the specification.
        /// </returns>
        Task<int> CountAsync(ISpecification<TAggregate> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// Determines whether any aggregate exists that satisfies the provided specification.
        /// </summary>
        /// <param name="specification">Specification that defines the query criteria.</param>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result is <c>true</c> if any matching
        /// aggregate exists; otherwise <c>false</c>.
        /// </returns>
        Task<bool> AnyAsync(ISpecification<TAggregate> specification, CancellationToken cancellationToken = default);
    }
}
