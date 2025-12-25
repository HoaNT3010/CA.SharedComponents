using SharedDomain.Abstractions;

namespace SharedDomain.Primitives
{
    /// <summary>
    /// Provides a base implementation for all domain entities, including identity,
    /// auditing, and soft-delete support.
    /// </summary>
    /// <typeparam name="TKey">The type of the unique identifier.</typeparam>
    /// <remarks>
    /// Following a <b>strict approach</b>, this base class forces the inclusion of
    /// <see cref="IFullAuditable"/> properties to ensure consistent tracking across the entire data model.
    /// </remarks>
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        /// <inheritdoc/>
        public TKey Id { get; set; } = default!;
        /// <inheritdoc/>
        public DateTimeOffset CreatedAt { get; set; }
        /// <inheritdoc/>
        public DateTimeOffset? UpdatedAt { get; set; }
        /// <inheritdoc/>
        public string? CreatedBy { get; set; }
        /// <inheritdoc/>
        public string? UpdatedBy { get; set; }
        /// <inheritdoc/>
        public string? DeletedBy { get; set; }
        /// <inheritdoc/>
        public bool IsDeleted { get; set; }
        /// <inheritdoc/>
        public DateTimeOffset? DeletedAt { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj is not Entity<TKey> other) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;
            if (IsTransient() || other.IsTransient()) return false;
            if (Id is null || other.Id is null) return false;
            return Id.Equals(other.Id);
        }
        public override int GetHashCode()
        {
            return Id is not null
                ? Id.GetHashCode()
                : base.GetHashCode();
        }
        public static bool operator ==(Entity<TKey>? left, Entity<TKey>? right) => Equals(left, right);
        public static bool operator !=(Entity<TKey>? left, Entity<TKey>? right) => !Equals(left, right);
        /// <summary>
        /// Marks the entity as logically deleted without physically removing it from the data store.
        /// </summary>
        /// <remarks>
        /// Soft deletion is used to preserve historical data and maintain referential integrity.
        /// Once an entity is soft-deleted, it should typically be excluded from normal queries
        /// but remain available for auditing or recovery purposes.
        /// </remarks>
        /// <param name="deletedAt">
        /// The point in time when the entity was deleted.
        /// If <c>null</c>, the current UTC time is used.
        /// </param>
        /// <param name="deletedBy">
        /// The identifier of the actor (user or system) that performed the deletion.
        /// </param>
        public virtual void SoftDelete(DateTimeOffset? deletedAt = null, string? deletedBy = null)
        {
            if (IsDeleted) return;
            IsDeleted = true;
            DeletedAt = deletedAt ?? DateTimeOffset.UtcNow;
            DeletedBy = deletedBy;
        }
        /// <summary>
        /// Restores a previously soft-deleted entity to its active state.
        /// </summary>
        /// <remarks>
        /// This operation reverses the effects of <see cref="SoftDelete"/> by clearing
        /// deletion-related metadata. It does not recreate the entity; it only changes
        /// its logical deletion state.
        /// </remarks>
        public virtual void Restore()
        {
            if (!IsDeleted) return;
            IsDeleted = false;
            DeletedAt = null;
            DeletedBy = null;
        }
        /// <summary>
        /// Determines whether the entity is transient (has not yet been assigned a persistent identity).
        /// </summary>
        /// <remarks>
        /// An entity is considered transient when its identifier is equal to the default value
        /// of <typeparamref name="TKey"/> (for example, <see cref="Guid.Empty"/> for <see cref="Guid"/>).
        /// <para/>
        /// Transient entities are not considered equal to any other entity, even if their identifiers
        /// are equal, because they have not yet been persisted.
        /// </remarks>
        protected bool IsTransient()
        {
            return EqualityComparer<TKey>.Default.Equals(Id, default!);
        }
    }
}
