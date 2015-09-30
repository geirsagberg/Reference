namespace Reference.Common.Contracts.Data
{
    /// <summary>
    /// Combines <seealso cref="IEntity"/>, <seealso cref="IEntityWithId"/> and <seealso cref="IVersioned"/>.
    /// </summary>
    public interface IVersionedEntityWithId : IEntityWithId, IVersioned { }
}