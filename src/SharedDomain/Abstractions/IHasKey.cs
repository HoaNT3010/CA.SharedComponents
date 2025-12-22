namespace SharedDomain.Abstractions
{
    /// <summary>
    /// Defines a contract for objects that possess a unique identifier.
    /// </summary>
    /// <typeparam name="TKey">The type of the unique identifier (e.g., <see cref="int"/>, <see cref="string"/>, or <see cref="Guid"/>).</typeparam>
    public interface IHasKey<TKey>
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        TKey Id { get; set; }
    }
}
