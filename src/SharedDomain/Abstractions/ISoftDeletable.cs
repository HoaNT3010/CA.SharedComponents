namespace SharedDomain.Abstractions
{
    /// <summary>
    /// Provides a mechanism for "soft deleting" entities, allowing records to be
    /// hidden from application logic without being physically removed from the database.
    /// </summary>
    public interface ISoftDeletable
    {
        /// <summary>
        /// Gets or sets a value indicating whether this entity is logically deleted.
        /// </summary>
        /// <value><c>true</c> if the entity is deleted; otherwise, <c>false</c>.</value>
        bool IsDeleted { get; set; }
        /// <summary>
        /// Gets or sets the date and time when the entity was marked as deleted.
        /// </summary>
        /// <remarks>
        /// This value should typically be <c>null</c> if <see cref="IsDeleted"/> is <c>false</c>.
        /// </remarks>
        DateTimeOffset? DeletedAt { get; set; }
    }
}
