using Reference.Common.Contracts.Data;

namespace Reference.BusinessLogic.Contracts
{
    public interface IExistingVersionedEntityModel<in TEntity> : IExistingEntityModel<TEntity>
        where TEntity : IVersionedEntityWithId
    {
        byte[] Version { get; }
    }
}