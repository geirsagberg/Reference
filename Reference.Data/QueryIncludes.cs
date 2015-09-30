using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Reference.Common.Contracts.Data;

namespace Reference.Data
{
    public class QueryIncludes<T> : IQueryIncludes<T> where T : class, IEntity
    {
        private readonly List<IPath> _expressions = new List<IPath>();

        public IQueryable<T> Include(IQueryable<T> queryable)
        {
            return _expressions.Aggregate(queryable, (current, expression) => expression.Include(current));
        }

        public IQueryIncludes<T> Add<TProperty>(Expression<Func<T, TProperty>> path)
        {
            _expressions.Add(new Path<TProperty>(path));
            return this;
        }

        private interface IPath
        {
            IQueryable<T> Include(IQueryable<T> queryable);
        }

        private class Path<TProperty> : IPath
        {
            private readonly Expression<Func<T, TProperty>> _path;

            public Path(Expression<Func<T, TProperty>> path)
            {
                _path = path;
            }

            public IQueryable<T> Include(IQueryable<T> queryable)
            {
                return queryable.Include(_path);
            }
        }
    }
}