using System;
using System.Linq.Expressions;

namespace Reference.Common.Contracts.Data
{
    public interface IQueryIncludesFactory
    {
        IQueryIncludes<T> Include<T, TProperty>(Expression<Func<T, TProperty>> path) where T : class, IEntity;
        IQueryIncludes<T> Include<T>() where T : class, IEntity;
    }
}