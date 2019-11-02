using SeizeTheDay.Core.DataAccess.Abstract.MySQL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;


namespace SeizeTheDay.Core.Concrete.MySQL
{
    public class MyEntityRepositoryBase<TEntity, TContext> : MyEntityRepository<TEntity>
        where TEntity : EntityObject, new()  //EntityObject is used for MySQL
        where TContext : ObjectContext, new()  //ObjectContext is used for MySQL
    {

        public List<TEntity> GetList()
        {
            using (TContext context = new TContext())
            {
                return context.CreateObjectSet<TEntity>().ToList();
            }
        }

        public void Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                context.CreateObjectSet<TEntity>().AddObject(entity);
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                context.Attach(entity);
                context.CreateObjectSet<TEntity>().DeleteObject(entity);
                context.SaveChanges();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                context.Attach(entity);
                context.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
                context.SaveChangesAsync();             
            }
        }
 
        public List<TEntity> Query(Expression<Func<TEntity, bool>> where = null)
        {
            using (TContext context = new TContext())
            {
                return context.CreateObjectSet<TEntity>().Where(where).ToList();
            }
        }

        public List<TEntity> Include(string includeTable)
        {
            using (TContext context = new TContext())
            {
                return context.CreateObjectSet<TEntity>().Include(includeTable).ToList();
            }
        }

        public TEntity Find(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.CreateObjectSet<TEntity>().Where(filter).FirstOrDefault();
            }
        }
      
        public List<TEntity> GetAllLazyLoad(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] children)
        {
            using (TContext context = new TContext())
            {
                var query = context.CreateObjectSet<TEntity>().ToList();
                foreach (var child in children)
                    query = context.CreateObjectSet<TEntity>().Include(child).Where(filter).ToList();              
                return query.ToList();
            }
        }


        public TEntity FirstOrDefault()
        {
            using (TContext context = new TContext())
            {
                return context.CreateObjectSet<TEntity>().FirstOrDefault();
            }
        }

        public TEntity GetFirstOrDefaultInclude(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] children)
        {
            using (TContext context = new TContext())
            {
                var query = context.CreateObjectSet<TEntity>().FirstOrDefault();
                foreach (var child in children)
                    query = context.CreateObjectSet<TEntity>().Include(child).Where(filter).FirstOrDefault();
                return query;
            }           
        }

        public  TEntity GetFirstOrDefaultInclude2(Expression<Func<TEntity, bool>> filter=null, params Expression<Func<TEntity, object>>[] includes)
        {
            using (TContext context = new TContext())
            {
                IQueryable<TEntity> query = context.CreateObjectSet<TEntity>();
                foreach (Expression<Func<TEntity, object>> include in includes)
                    query = query.Include(include);

                return query.FirstOrDefault(filter);
            }
        }

        public List<TEntity> TolistInclude(params Expression<Func<TEntity, object>>[] children)
        {
            using (TContext context = new TContext())
            {
                var query = context.CreateObjectSet<TEntity>().ToList();
                foreach (var child in children)
                    query = context.CreateObjectSet<TEntity>().Include(child).ToList();
                return query.ToList();
            }
        }

        public IQueryable<TEntity> GetAll()
        {
            using (TContext context = new TContext())
            {
                return context.CreateObjectSet<TEntity>().AsQueryable<TEntity>();
            }
        }

        public List<TEntity> StringInclude(params string[] children)
        {
            using (TContext context = new TContext())
            {
                var query = context.CreateObjectSet<TEntity>().ToList();
                foreach (var child in children)
                    query = context.CreateObjectSet<TEntity>().Include(child).ToList();
                return query.ToList();
            }
        }

        public List<TEntity> StringIncludeWithExpression(Expression<Func<TEntity, bool>> filter, params string[] children)
        {
            using (TContext context = new TContext())
            {
                var query = context.CreateObjectSet<TEntity>().ToList();
                foreach (var child in children)
                    query = context.CreateObjectSet<TEntity>().Include(child).Where(filter).ToList();
                return query.ToList();
            }
        }

        public TEntity StringIncludeSingle(params string[] children)
        {
            using (TContext context = new TContext())
            {
                var query = context.CreateObjectSet<TEntity>().FirstOrDefault();
                foreach (var child in children)
                    query = context.CreateObjectSet<TEntity>().Include(child).FirstOrDefault();
                return query;
            }
        }

        public TEntity StringIncludeSingleWithExpression(Expression<Func<TEntity, bool>> filter, params string[] children)
        {
            using (TContext context = new TContext())
            {
                var query = context.CreateObjectSet<TEntity>().FirstOrDefault();
                foreach (var child in children)
                    query = context.CreateObjectSet<TEntity>().Include(child).Where(filter).FirstOrDefault();
                return query;

            }
        }
    }
}
