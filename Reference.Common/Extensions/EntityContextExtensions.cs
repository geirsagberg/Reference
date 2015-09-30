using System;
using Reference.Common.Contracts;
using Reference.Common.Contracts.Data;
using Reference.Common.Exceptions;

namespace Reference.Common.Extensions
{
    public static class EntityContextExtensions
    {
        public static TEntity GetOrDie<TEntity>(this IEntityContext context, int id) where TEntity : class, IEntityWithId
        {
            var entity = context.Get<TEntity>(id);
            if (entity == null)
                throw new UserErrorException($"Finner ingen {typeof (TEntity).Name} med ID {id}");
            return entity;
        }
        public static TEntity GetOrDie<TEntity>(this IEntityContext context, int id, IQueryIncludes<TEntity> includePath) where TEntity : class, IEntityWithId
        {
            var entity = context.Get(id, includePath);
            if (entity == null)
                throw new UserErrorException($"Finner ingen {typeof(TEntity).Name} med ID {id}");
            return entity;
        }

        public static int DoInAutoSavingTransaction(this IEntityContext context, Action action)
        {
            using (var transaction = context.BeginTransaction()) {
                action();
                var status = context.SaveChanges();
                transaction.Complete();
                return status;
            }
        }
    }
}