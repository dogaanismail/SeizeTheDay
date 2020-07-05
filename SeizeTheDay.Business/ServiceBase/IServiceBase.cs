using SeizeTheDay.Core;
using SeizeTheDay.Core.Entities;
using SeizeTheDay.Core.Utilities.PageList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SeizeTheDay.Business.ServiceBase
{
    public interface IServiceBase<T> where T : BaseEntity
    {
        PagedList<T> GetPagedList(int pageIndex, int pageSize);
        PagedList<T> GetPagedList(int pageIndex, int pageSize, string includeTables);
        List<T> GetList();
        List<T> GetList(string includeTables);
        T GetById(int id);
        bool InsertWithResult(T entity);
        bool InsertWithResult(IEnumerable<T> entities);
        bool UpdateWithResult(T entity);
        bool UpdateWithResult(IEnumerable<T> entities);
        bool DeleteWithResult(T entity);
        bool DeleteWithResult(IEnumerable<T> entities);
        IQueryable<T> Table();
        IQueryable<T> Table(Expression<Func<T, bool>> whereCondition);
        IQueryable<T> Table(string includeTables);
    }
}
