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

        public static int DoInAutoSavingTransaction(this IEntityContext context, Action<IEntityContext> action)
        {
            using (var transaction = context.BeginTransaction()) {
                action(context);
                var status = context.SaveChanges();
                transaction.Complete();
                return status;
            }
        }

        public static T DoInAutoSavingTransaction<T>(this IEntityContext context, Func<IEntityContext, T> action) where T : IEntity
        {
            using (var transaction = context.BeginAutoSavingTransaction()) {
                var result = action(context);
                transaction.Complete();
                return result;
            }
        }

        public static ITransaction BeginAutoSavingTransaction(this IEntityContext repo)
        {
            return new AutoSavingTransaction(repo);
        }

        private class AutoSavingTransaction : ITransaction
        {
            private readonly IEntityContext _repo;
            private readonly ITransaction _trans;

            public AutoSavingTransaction(IEntityContext repo)
            {
                _repo = repo;
                _trans = repo.BeginTransaction();
            }

            public void Dispose()
            {
                _trans.Dispose();
            }

            public void Complete()
            {
                _repo.SaveChanges();
                _trans.Complete();
            }
        }
    }
}