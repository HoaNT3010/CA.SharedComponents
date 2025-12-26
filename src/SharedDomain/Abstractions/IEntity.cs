namespace SharedDomain.Abstractions
{
    /// <summary>
    /// Defines the base contract for a domain entity with a unique identifier and full auditing capabilities.
    /// </summary>
    /// <typeparam name="TKey">The type used for the entity's primary key.</typeparam>
    /// <remarks>
    /// This interface follows the <b>strict architectural approach</b>, requiring all implementing
    /// entities to provide full auditing (creation, modification, and soft-deletion) by default.
    /// </remarks>
    public interface IEntity<TKey> : IHasKey<TKey>, IFullAuditable;
}
