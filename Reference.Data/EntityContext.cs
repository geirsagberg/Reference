using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using EntityFramework.Extensions;
using Reference.Common.Contracts;
using Reference.Common.Contracts.Data;
using Reference.Common.Exceptions;
using Reference.Common.Extensions;

namespace Reference.Data
{
    internal partial class EntityContext : EntityContextBase, IEntityContext
    {
        private const int DefaultCommandTimeout = 120;
        private static readonly TimeSpan DefaultTransactionTimeout = TimeSpan.FromMinutes(10);

        // To ensure EntityFramework.SqlServer is included 
        // ReSharper disable once UnusedMember.Local
        private Type _instance = typeof (SqlProviderServices);

        static EntityContext()
        {
            // Disable automatic creation of database
            Database.SetInitializer<EntityContext>(null);
        }

        public EntityContext() : base("EntityContext")
        {
#if DEBUG
            Database.Log = s => Debug.Write(s);
#endif
            SetCommandTimeout();
        }

        public EntityContext(string name) : base(name)
        {
#if DEBUG
            Database.Log = s => Debug.Write(s);
#endif
            SetCommandTimeout();
        }

        public EntityContext(DbConnection connection) : base(connection)
        {
#if DEBUG
            Database.Log = s => Debug.Write(s);
#endif
            SetCommandTimeout();
        }

        public ITransaction BeginTransaction()
        {
            return BeginTransaction(DefaultTransactionTimeout);
        }

        public ITransaction BeginTransaction(TimeSpan timeout)
        {
            return new Transaction(timeout);
        }

        public T Add<T>(T entity) where T : class, IEntity
        {
            return Set<T>().Add(entity);
        }

        public IEnumerable<T> AddRange<T>(IEnumerable<T> entities) where T : class, IEntity
        {
            return Set<T>().AddRange(entities);
        }

        public T Remove<T>(int id) where T : class, IEntityWithId
        {
            var entity = Get<T>(id);
            if (entity == null)
                throw new ArgumentException($"{typeof (T).Name} with Id {id} not found");
            return Remove(entity);
        }

        public IEnumerable<T> RemoveRange<T>(IEnumerable<T> entities) where T : class, IEntity
        {
            return Set<T>().RemoveRange(entities);
        }

        T IEntityContext.Create<T>()
        {
            return Create<T>(true);
        }

        public T Create<T>(bool addToRepo)
            where T : class, IEntity
        {
            IDbSet<T> set = Set<T>();
            var entity = set.Create();
            if (addToRepo) {
                set.Add(entity);
            }
            return entity;
        }

        public T Get<T>(int id)
            where T : class, IEntity
        {
            return Set<T>().Find(id);
        }

        public T GetWithVersion<T>(int id, byte[] version)
            where T : class, IEntity, IVersioned
        {
            return GetWithVersion(Get<T>(id), version);
        }

        public T GetWithVersion<T>(int id, byte[] version, IQueryIncludes<T> pathToFetchEagerly)
            where T : class, IEntityWithId, IVersioned
        {
            return GetWithVersion(Get(id, pathToFetchEagerly), version);
        }

        public T GetWithVersion<T>(Func<IEntityContext, T> get, byte[] version)
            where T : class, IEntity, IVersioned
        {
            return GetWithVersion(get(this), version);
        }

        public T Get<T>(params object[] keyValues)
            where T : class, IEntity
        {
            return Set<T>().Find(keyValues);
        }

        public T Get<T>(int id, IQueryIncludes<T> pathToFetchEagerly)
            where T : class, IEntityWithId
        {
            return pathToFetchEagerly.Include(GetAll<T>()).FirstOrDefault(e => e.Id == id);
        }

        public IQueryable<T> GetAll<T>() where T : class, IEntity
        {
            return Set<T>();
        }

        public IQueryable<T> GetAll<T>(IQueryIncludes<T> pathToFetchEagerly)
            where T : class, IEntity
        {
            return pathToFetchEagerly.Include(GetAll<T>());
        }

        public T Remove<T>(T entity) where T : class, IEntity
        {
            return Set<T>().Remove(entity);
        }

        public IQueryIncludes<T> Include<T, TProperty>(Expression<Func<T, TProperty>> path)
            where T : class, IEntity
        {
            var include = Include<T>();
            include.Add(path);
            return include;
        }

        public IQueryIncludes<T> Include<T>()
            where T : class, IEntity
        {
            return new QueryIncludes<T>();
        }

        public override int SaveChanges()
        {
            try {
                SetModified();
                return base.SaveChanges();
            } catch (DbUpdateConcurrencyException e) {
                var entities = e.Entries
                    .Select(x => x.Entity)
                    .Select(x => $"{x.GetType()} with (Id={(x as IEntityWithId)?.Id.ToString() ?? "NULL"})");
                throw new EntityVersionConflictException("DbUpdateConcurrencyException for entities:" + string.Join(",", entities), e);
            } catch (DbEntityValidationException e) {
                var errorMessages = e.EntityValidationErrors
                    .SelectMany(eve => eve.ValidationErrors)
                    .Select(ve => ve.PropertyName + ": " + ve.ErrorMessage);
                var message = e.Message + "\nThe validation errors are:\n" + string.Join("\n", errorMessages);
                throw new DbEntityValidationException(message, e);
            }
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return Database.ExecuteSqlCommand(sql, parameters);
        }

        public void DisableLazyLoading()
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public void RemoveOptional<T, TProperty>(T entity, Expression<Func<T, TProperty>> property)
            where T : class, IEntity
            where TProperty : class, IEntity
        {
            Entry(entity).Reference(property).CurrentValue = null;
        }

        public int RemoveWhere<T>(Expression<Func<T, bool>> predicate) where T : class, IEntity => Set<T>().Where(predicate).Delete();

        public void RejectChanges(Type entityType = null)
        {
            foreach (var entry in ChangeTracker.Entries().WhereIf(entityType != null, e => e.Entity.GetType() == entityType)) {
                switch (entry.State) {
                    case EntityState.Modified: {
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    }
                    case EntityState.Deleted: {
                        entry.State = EntityState.Unchanged;
                        break;
                    }
                    case EntityState.Added: {
                        entry.State = EntityState.Detached;
                        break;
                    }
                }
            }
        }

        private void SetModified()
        {
            var created = ChangeTracker.Entries().Where(e => e.Entity is ICreated && e.State == EntityState.Added);
            foreach (var entity in created.Select(entry => ((ICreated) entry.Entity))) {
                entity.Opprettet = DateTimeOffset.Now;
            }
            var modified = ChangeTracker.Entries().Where(e => e.Entity is IModified && e.State.In(EntityState.Added, EntityState.Modified));
            foreach (var entity in modified.Select(entry => ((IModified) entry.Entity))) {
                entity.Endret = DateTimeOffset.Now;
            }
        }

        private void SetCommandTimeout()
        {
            Database.CommandTimeout = DefaultCommandTimeout;
        }

        private T GetWithVersion<T>(T entity, byte[] version)
            where T : class, IEntity, IVersioned
        {
            if (version == null || entity == null || entity.Version == version)
                return entity;
            entity.Version = version;
            var entry = ((IObjectContextAdapter) this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entity);
            entry.ChangeState(EntityState.Modified);
            entry.AcceptChanges();
            entry.ChangeState(EntityState.Modified);
            return entity;
        }

        /// <summary>
        ///     Upsert, use only for database seeding because it is unpredictable.
        /// </summary>
        internal void AddOrUpdate<T>(params T[] entities) where T : class, IEntity
        {
            Set<T>().AddOrUpdate(entities);
        }

        /// <summary>
        ///     Upsert, use only for database seeding because it is unpredictable.
        /// </summary>
        internal void AddOrUpdate<T>(Expression<Func<T, object>> identifierExpression, params T[] entities)
            where T : class, IEntity
        {
            Set<T>().AddOrUpdate(identifierExpression, entities);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            MapTypesToTable(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}