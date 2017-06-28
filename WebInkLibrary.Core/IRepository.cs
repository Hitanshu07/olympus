using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WebInkLibrary.Core.Data
{
    public interface IRepository<T> : IDisposable where T : class 
    {
        T GetById(object id);
        IEnumerable<T> GetAllWithCondition(Expression<Func<T, bool>> where);
        T Insert(T entity);
        T Update(T entity);
        T Delete(T entity);
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
        string GetTableName();
    }
}
