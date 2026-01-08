using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedDomain.Abstractions;
using SharedApplication.Persistence;

namespace SharedInfrastructure.Persistence
{
    /// <summary>
    /// Generic repository implementation using Entity Framework Core and Ardalis.Specification.
    /// Provides read and full-write operations for aggregate roots.
    /// </summary>
    /// <typeparam name="TAggregate">The aggregate root entity type.</typeparam>
    /// <typeparam name="TKey">The type of the aggregate's identifier.</typeparam>
    public class EFRepository<TAggregate, TKey>
        : IFullWriteRepository<TAggregate, TKey>,
        IReadRepository<TAggregate, TKey>
        where TAggregate : class, IAggregateRoot<TKey>
    {
        /// <summary>
        /// The EF <see cref="DbContext"/> used by this repository.
        /// </summary>
        protected readonly DbContext _context;
        /// <summary>
        /// The EF <see cref="DbSet{TAggregate}"/> for the requested aggregate type.
        /// </summary>
        protected readonly DbSet<TAggregate> _dbSet;
        /// <summary>
        /// Specification evaluator used to apply <see cref="ISpecification{TAggregate}"/>.
        /// </summary>
        protected readonly SpecificationEvaluator _specificationEvaluator;

        /// <summary>
        /// Creates a new instance of <see cref="EFRepository{TAggregate, TKey}"/>.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> to use for data access.</param>
        public EFRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TAggregate>();
            _specificationEvaluator = SpecificationEvaluator.Default;
        }

        /// <inheritdoc/>
        public virtual async Task AddAsync(TAggregate aggregate, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(aggregate, cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<bool> AnyAsync(ISpecification<TAggregate> specification, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification, true).AnyAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual void BulkDelete(IEnumerable<TAggregate> aggregates)
        {
            _dbSet.RemoveRange(aggregates);
        }

        /// <inheritdoc/>
        public virtual async Task BulkInsertAsync(IEnumerable<TAggregate> aggregates, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(aggregates, cancellationToken);
        }
        /// <inheritdoc/>
        public virtual void BulkUpdate(IEnumerable<TAggregate> aggregates)
        {
            _dbSet.UpdateRange(aggregates);
        }

        /// <inheritdoc/>
        public virtual async Task<int> CountAsync(ISpecification<TAggregate> specification, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification, true).CountAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual void Delete(TAggregate aggregate)
        {
            _dbSet.Remove(aggregate);
        }

        /// <inheritdoc/>
        public virtual async Task<TAggregate?> FirstOrDefaultAsync(ISpecification<TAggregate> specification, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<TAggregate?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.Id!.Equals(id), cancellationToken);
        }

        /// <inheritdoc/>
        public virtual async Task<IReadOnlyList<TAggregate>> ListAsync(ISpecification<TAggregate> specification, CancellationToken cancellationToken = default)
        {
            return await ApplySpecification(specification).ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public virtual void Update(TAggregate aggregate)
        {
            _dbSet.Update(aggregate);
        }

        /// <summary>
        /// Applies the given <see cref="ISpecification{TAggregate}"/> to the underlying <see cref="DbSet{TAggregate}"/>.
        /// </summary>
        /// <param name="specification">The specification describing query filters, includes and sorting.</param>
        /// <param name="evaluateCriteriaOnly">
        /// When <c>true</c>, only the specification criteria will be applied (no includes, ordering, or paging).
        /// Use this for count/any operations where includes are unnecessary.
        /// </param>
        /// <returns>An <see cref="IQueryable{TAggregate}"/> representing the specification-applied query.</returns>
        protected IQueryable<TAggregate> ApplySpecification(ISpecification<TAggregate> specification, bool evaluateCriteriaOnly = false)
        {
            return _specificationEvaluator.GetQuery(_dbSet, specification, evaluateCriteriaOnly);
        }
    }
}
