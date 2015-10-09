using Reference.Common.Contracts.Data;

namespace Reference.BusinessLogic.Contracts
{
    public interface IExistingEntityModel<in TEntity> : IEntityModel<TEntity> where TEntity : IEntityWithId
    {
        int Id { get; }
    }
}