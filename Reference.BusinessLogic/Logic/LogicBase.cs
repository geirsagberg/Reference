using JetBrains.Annotations;
using Reference.BusinessLogic.Contracts;
using Reference.Common.Contracts;
using Reference.Common.Contracts.Data;
using Reference.Common.Extensions;

namespace Reference.BusinessLogic.Logic
{
    public abstract class LogicBase
    {
        protected readonly IEntityContext Context;

        protected LogicBase(IEntityContext context)
        {
            Context = context;
        }
    }

    public abstract class LogicBase<TEntity> : LogicBase where TEntity : class, IVersionedEntityWithId
    {
        protected LogicBase(IEntityContext context) : base(context) {}

        [CanBeNull]
        public virtual TEntity Get(int id) => Context.Get<TEntity>(id);

        [NotNull]
        public virtual TEntity GetOrDie(int id) => Context.GetOrDie<TEntity>(id);

        public virtual TEntity Update(IExistingVersionedEntityModel<TEntity> model)
        {
            var entity = Context.GetWithVersion<TEntity>(model.Id, model.Version);
            model.CopyTo(entity);
            Context.SaveChanges();
            return entity;
        }

        public virtual TEntity Create(IEntityModel<TEntity> model)
        {
            var entity = Context.Create<TEntity>();
            model.CopyTo(entity);
            return entity;
        }
    }
}