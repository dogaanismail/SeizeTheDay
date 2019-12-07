using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;

namespace SeizeTheDay.Core.DataAccess.Abstract.MySQL
{
    public interface IMyEntityRepository<T> where T: EntityObject, new()  //EntityObject is used for MySQL
    {
        List<T> GetList();
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);

        IQueryable<T> GetAll();

        T FirstOrDefault();

        List<T> Query(Expression<Func<T, bool>> where);  //According to query
        List<T> GetAllLazyLoad(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] children);
        List<T> TolistInclude(params Expression<Func<T, object>>[] children);

        List<T> StringInclude(params string[] children); //string include toList
        List<T> StringIncludeWithExpression(Expression<Func<T, bool>> filter, params string[] children); //string include toList with exp.

        T StringIncludeSingle(params string[] children); //string include single
        T StringIncludeSingleWithExpression(Expression<Func<T, bool>> filter, params string[] children); //string include single with exp.

        T GetFirstOrDefaultInclude(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] children);
        T GetFirstOrDefaultInclude2(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] children);
        List<T> Include(string includeTable);
        T Find(Expression<Func<T, bool>> filter);
    }
}
