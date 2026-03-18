using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SharedApplication.Persistence;

namespace SharedInfrastructure.Persistence
{
    /// <summary>
    /// Entity Framework Core implementation of <see cref="IUnitOfWork"/> that manages a <see cref="DbContext"/>
    /// and database transactions.
    /// </summary>
    public sealed class EFUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private IDbContextTransaction? _transaction;

        /// <summary>
        /// Initializes a new instance of the <see cref="EFUnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/> used to persist changes and manage transactions.</param>
        public EFUnitOfWork(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a value indicating whether there is an active database transaction.
        /// </summary>
        public bool HasActiveTransaction => _transaction != null;

        /// <summary>
        /// Begins a new database transaction.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the operation to complete.</param>
        /// <returns>A task that represents the asynchronous begin transaction operation.</returns>
        /// <exception cref="InvalidOperationException">Thrown when a transaction is already active.</exception>
        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("A transaction has already started. Please commit or rollback the current transaction before starting a new one.");
            }
            _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        /// <summary>
        /// Commits the currently active transaction.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the operation to complete.</param>
        /// <returns>A task that represents the asynchronous commit operation.</returns>
        /// <exception cref="InvalidOperationException">Thrown when there is no active transaction to commit.</exception>
        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No active transaction available to commit.");
            }
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        /// <summary>
        /// Rolls back the currently active transaction.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the operation to complete.</param>
        /// <returns>A task that represents the asynchronous rollback operation.</returns>
        /// <exception cref="InvalidOperationException">Thrown when there is no active transaction to rollback.</exception>
        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No active transaction available to roll back.");
            }
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        /// <summary>
        /// Persists all changes made in the current <see cref="DbContext"/> to the underlying database.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the operation to complete.</param>
        /// <returns>
        /// A task that completes when the save operation finishes. The result is the number of state entries written to
        /// the underlying database.
        /// </returns>
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
