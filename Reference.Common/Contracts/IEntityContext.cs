using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Reference.Common.Contracts.Data;

namespace Reference.Common.Contracts
{
    public interface IEntityContext : IDisposable, IQueryIncludesFactory
    {
        ITransaction BeginTransaction();

        ITransaction BeginTransaction(TimeSpan timeout);

        T Get<T>(int id) where T : class, IEntity;

        T Get<T>(params object[] keyValues) where T : class, IEntity;

        T Get<T>(int id, IQueryIncludes<T> pathToFetchEagerly) where T : class, IEntityWithId;

        T GetWithVersion<T>(int id, byte[] version) where T : class, IEntity, IVersioned;

        T GetWithVersion<T>(int id, byte[] version, IQueryIncludes<T> pathToFetchEagerly) where T : class, IEntityWithId, IVersioned;

        T GetWithVersion<T>(Func<IEntityContext, T> get, byte[] version) where T : class, IEntity, IVersioned;

        IQueryable<T> GetAll<T>() where T : class, IEntity;

        IQueryable<T> GetAll<T>(IQueryIncludes<T> pathToFetchEagerly) where T : class, IEntity;

        T Add<T>(T entity) where T : class, IEntity;

        T Create<T>() where T : class, IEntity;

        T Create<T>(bool addToRepo) where T : class, IEntity;

        T Remove<T>(T entity) where T : class, IEntity;

        T Remove<T>(int id) where T : class, IEntityWithId;

        IEnumerable<T> RemoveRange<T>(IEnumerable<T> entities) where T : class, IEntity;

        void RemoveOptional<T, TProperty>(T entity, Expression<Func<T, TProperty>> property)
            where T : class, IEntity
            where TProperty : class, IEntity;

        int SaveChanges();

        int ExecuteSqlCommand(string sql, params object[] parameters);
        IEnumerable<T> AddRange<T>(IEnumerable<T> entities) where T : class, IEntity;
        int RemoveWhere<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity;

        void DisableLazyLoading();

        /// <summary>
        ///     Discard any changes to the underlying entities. If a type is passed, only changes to entities of that type are
        ///     discarded.
        /// </summary>
        void RejectChanges(Type entityType = null);

    }
}