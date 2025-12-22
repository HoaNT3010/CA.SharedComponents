namespace SharedDomain.Abstractions
{
    /// <summary>
    /// Defines a contract for tracking the identity of the users who created, 
    /// modified, or deleted the entity.
    /// </summary>
    public interface IUserAuditable
    {
        /// <summary>
        /// Gets or sets the identifier of the user who created the entity.
        /// </summary>
        /// <value>A <see cref="string"/> representing the user ID or username.</value>
        string? CreatedBy { get; set; }
        /// <summary>
        /// Gets or sets the identifier of the user who last updated the entity.
        /// </summary>
        /// <value>A <see cref="string"/> representing the user ID or username.</value>
        string? UpdatedBy { get; set; }
        /// <summary>
        /// Gets or sets the identifier of the user who performed the soft delete.
        /// </summary>
        /// <remarks>
        /// This property should be <c>null</c> until the entity's <c>IsDeleted</c> property is set to <c>true</c>.
        /// </remarks>
        string? DeletedBy { get; set; }
    }
}
