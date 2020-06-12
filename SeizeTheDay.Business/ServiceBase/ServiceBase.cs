using SeizeTheDay.Core.DataAccess.Abstract;
using SeizeTheDay.Core.Entities;
using SeizeTheDay.Core.Utilities.PageList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace SeizeTheDay.Business.ServiceBase
{
    public abstract class ServiceBase<T> : IServiceBase<T> where T : BaseEntity
    {
        #region Fields
        public readonly IRepository<T> _repository;
        #endregion

        #region Ctor

        public ServiceBase(IRepository<T> repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods

        public virtual PagedList<T> GetPagedList(int pageIndex, int pageSize, string includeTables = "")
        {
            return new PagedList<T>(Table(includeTables).OrderBy(p => p.Id), pageIndex, pageSize);
        }
        public virtual List<T> GetList(string includeTables = "")
        {
            return Table(includeTables).ToList();
        }
        public virtual T GetById(int id)
        {
            if (id == 0)
                return null;

            return _repository.GetById(id);
        }
        public virtual void Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(typeof(T).Name.ToLowerInvariant());

            _repository.Insert(entity);
        }
        public virtual void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(typeof(T).Name.ToLowerInvariant());

            _repository.Update(entity);
        }
        public virtual void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(typeof(T).Name.ToLowerInvariant());

            _repository.Delete(entity);
        }
        public virtual IQueryable<T> Table(string includeTables = "")
        {
            if (!string.IsNullOrEmpty(includeTables))
            {
                return _repository.Table.Include(includeTables);
            }
            else
                return _repository.Table;
        }
        public virtual IQueryable<T> Table(Expression<Func<T, bool>> whereCondition)
        {
            return Table().Where(whereCondition);
        }
        public virtual bool Any(Expression<Func<T, bool>> anyCondition)
        {
            return Table().Any(anyCondition);
        }
        public virtual PagedList<T> GetPagedList(int pageIndex, int pageSize)
        {
            return GetPagedList(pageIndex, pageSize, string.Empty);
        }
        public virtual List<T> GetList()
        {
            return GetList(string.Empty);
        }
        public virtual IQueryable<T> Table()
        {
            return Table(string.Empty);
        }
        public bool InsertWithResult(T entity)
        {
            return _repository.InsertWithResult(entity);
        }
        public bool InsertWithResult(IEnumerable<T> entities)
        {
            return _repository.InsertWithResult(entities);
        }
        public bool UpdateWithResult(T entity)
        {
            return _repository.UpdateWithResult(entity);
        }
        public bool UpdateWithResult(IEnumerable<T> entities)
        {
            return _repository.UpdateWithResult(entities);
        }
        public bool DeleteWithResult(T entity)
        {
            return _repository.DeleteWithResult(entity);
        }
        public bool DeleteWithResult(IEnumerable<T> entities)
        {
            return _repository.DeleteWithResult(entities);
        }

        #endregion
    }
}
