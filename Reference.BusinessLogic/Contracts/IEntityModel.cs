using Reference.Common.Contracts.Data;

namespace Reference.BusinessLogic.Contracts
{
    public interface IEntityModel<in TEntity> where TEntity : IEntity
    {
        void CopyTo(TEntity entity);
    }
}