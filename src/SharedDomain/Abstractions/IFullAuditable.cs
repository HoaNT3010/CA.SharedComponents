namespace SharedDomain.Abstractions
{
    /// <summary>
    /// A comprehensive contract that combines time-based auditing, user-based auditing,
    /// and soft-delete capabilities.
    /// </summary>
    /// <remarks>
    /// Entities implementing this interface provide a full audit trail of their lifecycle.
    /// </remarks>
    public interface IFullAuditable : IAuditable, IUserAuditable, ISoftDeletable;
}
