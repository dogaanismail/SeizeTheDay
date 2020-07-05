//using SeizeTheDay.Core.DataAccess.Abstract;
//using SeizeTheDay.Core.Entities;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Data.Entity.Core.Objects;
//using System.Data.Entity.Infrastructure;
//using System.Data.Entity.Validation;
//using System.Linq;

//namespace SeizeTheDay.Core.DataAccess.Concrete.MySQL
//{
//    /// <summary>
//    /// MySQL repository
//    /// </summary>
//    public partial class MyRepository<T> : IRepository<T> where T : BaseEntity
//    {
//        #region Fields

//        private readonly IDbContext _context;
//        private ObjectSet<T> _entities;

//        #endregion

//        #region Ctor

//        /// <summary>
//        /// Ctor
//        /// </summary>
//        /// <param name="context">Object context</param>
//        public MyRepository(IDbContext context)
//        {
//            this._context = context;
//        }

//        #endregion

//        #region Utilities

//        /// <summary>
//        /// Get full error
//        /// </summary>
//        /// <param name="exc">Exception</param>
//        /// <returns>Error</returns>
//        protected string GetFullErrorText(DbEntityValidationException exc)
//        {
//            var msg = string.Empty;
//            foreach (var validationErrors in exc.EntityValidationErrors)
//                foreach (var error in validationErrors.ValidationErrors)
//                    msg += string.Format("Property: {0} Error: {1}", error.PropertyName, error.ErrorMessage) + Environment.NewLine;
//            return msg;
//        }

//        #endregion

//        #region Methods

//        /// <summary>
//        /// Gets entity by identifier
//        /// </summary>
//        /// <param name="id">Identifier</param>
//        /// <returns>Entity</returns>
//        public virtual T GetById(object id)
//        {
//            try
//            {
//                this._context.AutoDetectChangesEnabled = false;
//                return this.Entities.FirstOrDefault(x => x.Id == (int)id);
//            }
//            finally
//            {
//                this._context.AutoDetectChangesEnabled = true;
//            }
//        }

//        /// <summary>
//        /// Inserts an entity
//        /// </summary>
//        /// <param name="entity">Entity</param>
//        public virtual void Insert(T entity)
//        {
//            try
//            {
//                if (entity == null)
//                    throw new ArgumentNullException("entity");

//                this.Entities.AddObject(entity);

//                CheckConnectionState();

//                this._context.SaveChanges();
//            }
//            catch (DbEntityValidationException dbEx)
//            {
//                throw new Exception(GetFullErrorText(dbEx), dbEx);
//            }
//            catch (Exception ex)
//            {

//            }
//        }

//        /// <summary>
//        /// Inserts an entity with a result
//        /// </summary>
//        /// <param name="entity"></param>
//        /// <returns></returns>
//        public virtual bool InsertWithResult(T entity)
//        {
//            bool result = false;
//            try
//            {
//                if (entity == null)
//                    throw new ArgumentNullException("entity");

//                this.Entities.AddObject(entity);

//                CheckConnectionState();

//                result = this._context.SaveChanges() > 0;
//            }
//            catch (DbEntityValidationException dbEx)
//            {
//                throw new Exception(GetFullErrorText(dbEx), dbEx);
//            }
//            catch (Exception ex)
//            {

//            }

//            return result;
//        }

//        /// <summary>
//        /// Inserts entities
//        /// </summary>
//        /// <param name="entities">Entities</param>
//        public virtual void Insert(IEnumerable<T> entities)
//        {
//            try
//            {
//                if (entities == null)
//                    throw new ArgumentNullException("entities");

//                foreach (var entity in entities)
//                    this.Entities.AddObject(entity);

//                CheckConnectionState();

//                this._context.SaveChanges();
//            }
//            catch (DbEntityValidationException dbEx)
//            {
//                throw new Exception(GetFullErrorText(dbEx), dbEx);
//            }
//            catch (Exception ex)
//            {

//            }
//        }

//        /// <summary>
//        /// Inserts entities with a result
//        /// </summary>
//        /// <param name="entities"></param>
//        /// <returns></returns>
//        public bool InsertWithResult(IEnumerable<T> entities)
//        {
//            bool result = false;
//            try
//            {
//                if (entities == null)
//                    throw new ArgumentNullException("entities");

//                foreach (var entity in entities)
//                    this.Entities.AddObject(entity);

//                CheckConnectionState();

//                result = this._context.SaveChanges() > 0;
//            }
//            catch (DbEntityValidationException dbEx)
//            {
//                throw new Exception(GetFullErrorText(dbEx), dbEx);
//            }
//            catch (Exception ex)
//            {

//            }
//            return result;
//        }

//        /// <summary>
//        /// Updates an entity
//        /// </summary>
//        /// <param name="entity">Entity</param>
//        public virtual void Update(T entity)
//        {
//            try
//            {
//                if (entity == null)
//                    throw new ArgumentNullException("entity");

//                CheckConnectionState();

//                this._context.SaveChanges();
//            }
//            catch (DbEntityValidationException dbEx)
//            {
//                throw new Exception(GetFullErrorText(dbEx), dbEx);
//            }
//            catch (DbUpdateConcurrencyException ex)
//            {
//                ex.Entries.Single().Reload();
//                this._context.SaveChanges();
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }
//        }

//        /// <summary>
//        /// Updates entities
//        /// </summary>
//        /// <param name="entities">Entities</param>
//        public virtual void Update(IEnumerable<T> entities)
//        {
//            try
//            {
//                if (entities == null)
//                    throw new ArgumentNullException("entities");

//                CheckConnectionState();

//                this._context.SaveChanges();
//            }
//            catch (DbEntityValidationException dbEx)
//            {
//                throw new Exception(GetFullErrorText(dbEx), dbEx);
//            }
//            catch (DbUpdateConcurrencyException ex)
//            {
//                ex.Entries.Single().Reload();
//                this._context.SaveChanges();
//            }
//            catch (Exception ex)
//            {

//            }
//        }

//        /// <summary>
//        /// Updates entities with a result
//        /// </summary>
//        /// <param name="entities"></param>
//        /// <returns></returns>
//        public bool UpdateWithResult(IEnumerable<T> entities)
//        {
//            bool result = false;
//            try
//            {
//                if (entities == null)
//                    throw new ArgumentNullException("entities");

//                CheckConnectionState();

//                result = this._context.SaveChanges() > 0;
//            }
//            catch (DbEntityValidationException dbEx)
//            {
//                throw new Exception(GetFullErrorText(dbEx), dbEx);
//            }
//            catch (DbUpdateConcurrencyException ex)
//            {
//                ex.Entries.Single().Reload();
//                this._context.SaveChanges();
//            }
//            catch (Exception ex)
//            {

//            }
//            return result;
//        }

//        /// <summary>
//        /// Updates an entity with a result
//        /// </summary>
//        /// <param name="entity"></param>
//        /// <returns></returns>
//        public bool UpdateWithResult(T entity)
//        {
//            bool result = false;
//            try
//            {
//                if (entity == null)
//                    throw new ArgumentNullException("entities");

//                CheckConnectionState();

//                result = this._context.SaveChanges() > 0;
//            }
//            catch (DbEntityValidationException dbEx)
//            {
//                throw new Exception(GetFullErrorText(dbEx), dbEx);
//            }
//            catch (DbUpdateConcurrencyException ex)
//            {
//                ex.Entries.Single().Reload();
//                this._context.SaveChanges();
//            }
//            catch (Exception ex)
//            {

//            }
//            return result;
//        }

//        /// <summary>
//        /// Deletes an entity
//        /// </summary>
//        /// <param name="entity">Entity</param>
//        public virtual void Delete(T entity)
//        {
//            try
//            {
//                if (entity == null)
//                    throw new ArgumentNullException("entity");

//                this.Entities.DeleteObject(entity);

//                CheckConnectionState();

//                this._context.SaveChanges();
//            }
//            catch (DbEntityValidationException dbEx)
//            {
//                throw new Exception(GetFullErrorText(dbEx), dbEx);
//            }
//            catch (Exception ex)
//            {
//                this._context.ForceDelete(entity);
//            }
//        }

//        /// <summary>
//        /// Deletes an entity with a result
//        /// </summary>
//        /// <param name="entity"></param>
//        /// <returns></returns>
//        public virtual bool DeleteWithResult(T entity)
//        {
//            bool result = false;
//            try
//            {
//                if (entity == null)
//                    throw new ArgumentNullException("entity");

//                this.Entities.DeleteObject(entity);

//                CheckConnectionState();

//                result = this._context.SaveChanges() > 0;
//            }
//            catch (DbEntityValidationException dbEx)
//            {
//                throw new Exception(GetFullErrorText(dbEx), dbEx);
//            }
//            catch (Exception ex)
//            {
//                this._context.ForceDelete(entity);
//                result = true;
//            }

//            return result;
//        }

//        /// <summary>
//        /// Deletes entities
//        /// </summary>
//        /// <param name="entities">Entities</param>
//        public virtual void Delete(IEnumerable<T> entities)
//        {
//            try
//            {
//                if (entities == null)
//                    throw new ArgumentNullException("entities");

//                foreach (var entity in entities)
//                    this.Entities.DeleteObject(entity);

//                CheckConnectionState();

//                this._context.SaveChanges();
//            }
//            catch (DbEntityValidationException dbEx)
//            {
//                throw new Exception(GetFullErrorText(dbEx), dbEx);
//            }
//            catch (Exception ex)
//            {

//            }
//        }

//        /// <summary>
//        /// Deletes entities with a result
//        /// </summary>
//        /// <param name="entities"></param>
//        /// <returns></returns>
//        public bool DeleteWithResult(IEnumerable<T> entities)
//        {
//            bool result = false;
//            try
//            {
//                if (entities == null)
//                    throw new ArgumentNullException("entities");

//                foreach (var entity in entities)
//                    this.Entities.DeleteObject(entity);

//                CheckConnectionState();

//                result = this._context.SaveChanges() > 0;
//            }
//            catch (DbEntityValidationException dbEx)
//            {
//                throw new Exception(GetFullErrorText(dbEx), dbEx);
//            }
//            catch (Exception ex)
//            {

//            }
//            return result;
//        }

//        public virtual bool ExecuteSqlQuery(string query)
//        {
//            bool result = false;
//            try
//            {
//                if (string.IsNullOrEmpty((query)))
//                    return result;

//                ObjectContext dbContext = (ObjectContext)this._context;
//                var connection = dbContext.Connection;

//                if (connection.State != ConnectionState.Open)
//                    connection.Open();

//                result = dbContext.ExecuteStoreCommand(query) > 0;

//                connection.Close();
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }

//            return result;
//        }

//        private void CheckConnectionState()
//        {
//            var connection = ((ObjectContext)this._context).Connection;

//            if (connection.State != ConnectionState.Open)
//                ((ObjectContext)this._context).Connection.Open();
//        }

//        public virtual int InsertTotatly(List<T> entityList)
//        {
//            var oldAutoDetectChangesEnabledStatus = _context.AutoDetectChangesEnabled;
//            _context.AutoDetectChangesEnabled = false;
//            foreach (var entity in entityList)
//            {
//                if (entity == null)
//                    throw new ArgumentNullException("entity");
//                this.Entities.AddObject(entity);
//            }
//            CheckConnectionState();

//            int recordId = _context.SaveChanges();
//            _context.AutoDetectChangesEnabled = oldAutoDetectChangesEnabledStatus;

//            return recordId;
//        }

//        #endregion

//        #region Properties

//        /// <summary>
//        /// Gets a table
//        /// </summary>
//        public virtual IQueryable<T> Table
//        {
//            get
//            {
//                return this.Entities;
//            }
//        }

//        /// <summary>
//        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
//        /// </summary>
//        public virtual IQueryable<T> TableNoTracking
//        {
//            get
//            {
//                return this.Entities.AsNoTracking();
//            }
//        }

//        /// <summary>
//        /// Entities
//        /// </summary>
//        protected virtual ObjectSet<T> Entities
//        {
//            get
//            {
//                if (_entities == null)
//                    _entities = _context.Set<T>();
//                return _entities;
//            }
//        }

//        #endregion
//    }
//}
