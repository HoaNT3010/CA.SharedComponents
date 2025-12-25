namespace SharedDomain.Abstractions
{
    /// <summary>
    /// Defines a contract for tracking the creation and modification timestamps of an entity.
    /// </summary>
    public interface IAuditable
    {
        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        DateTimeOffset CreatedAt { get; set; }
        /// <summary>
        /// Gets or sets the date and time when the entity was last updated.
        /// </summary>
        /// <remarks>
        /// This property should be <c>null</c> if the entity has never been modified
        /// after its initial creation.
        /// </remarks>
        DateTimeOffset? UpdatedAt { get; set; }
    }
}
