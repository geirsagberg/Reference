using System;
using System.Linq;
using System.Linq.Expressions;

namespace Reference.Common.Contracts.Data
{
    public interface IQueryIncludes<T> where T : IEntity
    {
        IQueryable<T> Include(IQueryable<T> queryable);
        IQueryIncludes<T> Add<TProperty>(Expression<Func<T, TProperty>> path);
    }
}