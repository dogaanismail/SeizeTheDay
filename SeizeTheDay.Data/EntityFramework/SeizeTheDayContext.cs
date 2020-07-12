using Microsoft.AspNet.Identity.EntityFramework;
using SeizeTheDay.Core.DataAccess.Abstract;
using SeizeTheDay.Core.Domain.Identity;
using SeizeTheDay.Core.Entities;
using SeizeTheDay.Entities.Mapping.Chats;
using SeizeTheDay.Entities.Mapping.Country;
using SeizeTheDay.Entities.Mapping.Forums;
using SeizeTheDay.Entities.Mapping.Friends;
using SeizeTheDay.Entities.Mapping.Identity;
using SeizeTheDay.Entities.Mapping.Module;
using SeizeTheDay.Entities.Mapping.Notification;
using SeizeTheDay.Entities.Mapping.ProfileVisitor;
using SeizeTheDay.Entities.Mapping.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SeizeTheDay.Data.EntityFramework
{
    public class SeizeTheDayContext : IdentityDbContext<AppUser, AppRole, int,
        AppUserLogin, AppUserRole, AppUserClaim>, IDbContext
    {
        #region Ctor
        public SeizeTheDayContext()
            : base("name=SeizeTheDayContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public static SeizeTheDayContext Create()
        {
            return new SeizeTheDayContext();
        }
        static SeizeTheDayContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SeizeTheDayContext>());
        }
        #endregion

        #region Utilities
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ChatGroupMap());
            modelBuilder.Configurations.Add(new ChatGroupUserMap());
            modelBuilder.Configurations.Add(new ChatMap());
            modelBuilder.Configurations.Add(new CountryMap());
            modelBuilder.Configurations.Add(new ForumCommentLikeMap());
            modelBuilder.Configurations.Add(new ForumMap());
            modelBuilder.Configurations.Add(new ForumPostCommentMap());
            modelBuilder.Configurations.Add(new ForumPostLikeMap());
            modelBuilder.Configurations.Add(new ForumPostMap());
            modelBuilder.Configurations.Add(new ForumTopicMap());
            modelBuilder.Configurations.Add(new PortalMessageMap());
            modelBuilder.Configurations.Add(new FriendMap());
            modelBuilder.Configurations.Add(new FriendRequestMap());
            modelBuilder.Configurations.Add(new ModuleMap());
            modelBuilder.Configurations.Add(new NotificationMap());
            modelBuilder.Configurations.Add(new ProfileVisitorMap());
            modelBuilder.Configurations.Add(new SettingMap());
            modelBuilder.Configurations.Add(new AppRoleMap());
            modelBuilder.Configurations.Add(new AppUserClaimMap());
            modelBuilder.Configurations.Add(new AppUserDetailMap());
            modelBuilder.Configurations.Add(new AppUserLoginMap());
            modelBuilder.Configurations.Add(new AppUserMap());
            modelBuilder.Configurations.Add(new AppUserRoleMap());
            base.OnModelCreating(modelBuilder);

            //This is used for rename for identity entity tables.
            modelBuilder.Entity<AppUser>().ToTable("AppUser");
            modelBuilder.Entity<AppRole>().ToTable("AppRole");
            modelBuilder.Entity<AppUserRole>().ToTable("AppUserRole");
            modelBuilder.Entity<AppUserClaim>().ToTable("AppUserClaim");
            modelBuilder.Entity<AppUserLogin>().ToTable("AppUserLogin");
        }

        /// <summary>
        /// Attach an entity to the context or return an already attached entity (if it was already attached)
        /// </summary>
        /// <typeparam name="TEntity">TEntity</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns>Attached entity</returns>
        protected virtual TEntity AttachEntityToContext<TEntity>(TEntity entity) where TEntity : BaseEntity, new()
        {
            var alreadyAttached = Set<TEntity>().Local.FirstOrDefault(x => x.Id == entity.Id);
            if (alreadyAttached == null)
            {
                Set<TEntity>().Attach(entity);
                return entity;
            }
            return alreadyAttached;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create database script
        /// </summary>
        /// <returns>SQL to generate database</returns>
        public string CreateDatabaseScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        }

        /// <summary>
        /// Get DbSet
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>DbSet</returns>
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        /// <summary>
        /// Execute stores procedure and load a list of entities at the end
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="commandText">Command text</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>Entities</returns>
        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            //add parameters to command
            if (parameters != null && parameters.Length > 0)
            {
                for (int i = 0; i <= parameters.Length - 1; i++)
                {
                    var p = parameters[i] as DbParameter;
                    if (p == null)
                        throw new Exception("Not support parameter type");

                    commandText += i == 0 ? " " : ", ";

                    commandText += "@" + p.ParameterName;
                    if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                    {
                        //output parameter
                        commandText += " output";
                    }
                }
            }
            var result = this.Database.SqlQuery<TEntity>(commandText, parameters).ToList();

            //performance hack applied as described here - http://www.nopcommerce.com/boards/t/25483/fix-very-important-speed-improvement.aspx
            bool acd = this.Configuration.AutoDetectChangesEnabled;
            try
            {
                this.Configuration.AutoDetectChangesEnabled = false;

                for (int i = 0; i < result.Count; i++)
                    result[i] = AttachEntityToContext(result[i]);
            }
            finally
            {
                this.Configuration.AutoDetectChangesEnabled = acd;
            }

            return result;
        }

        public IList<T> ExecuteStoredProcedure<T>(string commandText, params object[] parameters)
        {
            //add parameters to command
            if (parameters != null && parameters.Length > 0)
            {
                for (int i = 0; i <= parameters.Length - 1; i++)
                {
                    var p = parameters[i] as DbParameter;
                    if (p == null)
                        throw new Exception("Not support parameter type");

                    commandText += i == 0 ? " " : ", ";

                    commandText += "@" + p.ParameterName;
                    if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                    {
                        //output parameter
                        commandText += " output";
                    }
                }
            }
            var result = this.Database.SqlQuery<T>(commandText, parameters).ToList();

            return result;
        }

        /// <summary>
        /// Creates a raw SQL query that will return elements of the given generic type.  The type can be any type that has properties that match the names of the columns returned from the query, or can be a simple primitive type. The type does not have to be an entity type. The results of this query are never tracked by the context even if the type of object returned is an entity type.
        /// </summary>
        /// <typeparam name="TElement">The type of object returned by the query.</typeparam>
        /// <param name="sql">The SQL query string.</param>
        /// <param name="parameters">The parameters to apply to the SQL query string.</param>
        /// <returns>Result</returns>
        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            this.Database.CommandTimeout = 1800;
            return this.Database.SqlQuery<TElement>(sql, parameters);
        }

        /// <summary>
        /// Executes the given DDL/DML command against the database.
        /// </summary>
        /// <param name="sql">The command string</param>
        /// <param name="doNotEnsureTransaction">false - the transaction creation is not ensured; true - the transaction creation is ensured.</param>
        /// <param name="timeout">Timeout value, in seconds. A null value indicates that the default value of the underlying provider will be used</param>
        /// <param name="parameters">The parameters to apply to the command string.</param>
        /// <returns>The result returned by the database after executing the command.</returns>
        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            int? previousTimeout = null;
            if (timeout.HasValue)
            {
                //store previous timeout
                previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }

            var transactionalBehavior = doNotEnsureTransaction
                ? TransactionalBehavior.DoNotEnsureTransaction
                : TransactionalBehavior.EnsureTransaction;
            var result = this.Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);

            if (timeout.HasValue)
            {
                //Set previous timeout back
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
            }

            //return result
            return result;
        }

        /// <summary>
        /// Detach an entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public void Detach(object entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            ((IObjectContextAdapter)this).ObjectContext.Detach(entity);
        }

        #endregion Methods

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether proxy creation setting is enabled (used in EF)
        /// </summary>
        public virtual bool ProxyCreationEnabled
        {
            get
            {
                return this.Configuration.ProxyCreationEnabled;
            }
            set
            {
                this.Configuration.ProxyCreationEnabled = value;
            }
        }

        public virtual bool ValidateOnSaveEnabled
        {
            get
            {
                return this.Configuration.ValidateOnSaveEnabled;
            }
            set
            {
                this.Configuration.ValidateOnSaveEnabled = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether auto detect changes setting is enabled (used in EF)
        /// </summary>
        public virtual bool AutoDetectChangesEnabled
        {
            get
            {
                return this.Configuration.AutoDetectChangesEnabled;
            }
            set
            {
                this.Configuration.AutoDetectChangesEnabled = value;
            }
        }

        public virtual bool LazyLoadingEnabled
        {
            get
            {
                return this.Configuration.LazyLoadingEnabled;
            }
            set
            {
                this.Configuration.LazyLoadingEnabled = false;
            }
        }

        #endregion Properties

        #region Functions
        public DataTable ToDataTable<T>(List<T> data, string tableName)
        {
            var props = TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable
            {
                TableName = tableName
            };
            try
            {
                for (int i = 0; i < props.Count; i++)
                {
                    PropertyDescriptor prop = props[i];
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }
                if (table.Columns["Id"] == null)
                {
                    var column = new DataColumn();
                    column.ColumnName = "Id";
                    column.DataType = System.Type.GetType("System.Int32");
                    column.AutoIncrement = true;
                    column.AutoIncrementSeed = 1;
                    column.AutoIncrementStep = 1;
                    table.Columns.Add(column);
                }
                object[] values = new object[props.Count];
                foreach (T item in data)
                {
                    var row = table.NewRow();
                    for (int i = 0; i < values.Length; i++)
                    {
                        row[i] = props[i].GetValue(item);
                    }
                    table.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return table;
        }

        public string CreateOrTruncateTable(DataTable tbl)
        {
            var queryBuilder = new StringBuilder();
            queryBuilder.AppendLine("IF NOT EXISTs(SELECT 1 FROM sys.tables WHERE NAME = '" + tbl.TableName + "')");
            queryBuilder.AppendLine("BEGIN");
            queryBuilder.AppendLine("CREATE TABLE [" + tbl.TableName + "] (");

            var props = new List<string>();
            for (int i = 0; i < tbl.Columns.Count; i++)
            {
                var prop = "";
                prop += ("[" + tbl.Columns[i].ColumnName + "]");
                if (tbl.Columns[i].DataType.ToString().Contains("System.Int32"))
                    prop += (" int ");
                else if (tbl.Columns[i].DataType.ToString().Contains("System.DateTime"))
                    prop += (" datetime ");
                else if (tbl.Columns[i].DataType.ToString().Contains("System.Decimal"))
                    prop += (" decimal(18,4) ");
                else
                    prop += (" nvarchar(" + (tbl.Columns[i].MaxLength > 0 ? tbl.Columns[i].MaxLength.ToString() : "max") + ") ");
                props.Add(prop);
            }

            queryBuilder.AppendLine(string.Join(",", props));
            queryBuilder.AppendLine(") END");
            queryBuilder.AppendLine("ELSE BEGIN TRUNCATE TABLE [" + tbl.TableName + "] END");

            return queryBuilder.ToString();
        }

        public void BulkInsert<T>(List<T> list, string tableName, bool AllowTruncate = true)
        {
            var table = ToDataTable(list, tableName);
            if (AllowTruncate)
            {
                ExecuteSqlCommand(CreateOrTruncateTable(table));
            }
            var con = (SqlConnection)this.Database.Connection;
            SqlBulkCopyColumnMapping map;
            using (var bulkCopy = new SqlBulkCopy(con))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                bulkCopy.BatchSize = list.Count;
                bulkCopy.DestinationTableName = table.TableName;

                if (table.TableName == "AuditLog")
                {
                    map = new SqlBulkCopyColumnMapping(table.Columns["Id"].ColumnName, "Id");
                    bulkCopy.ColumnMappings.Add(map);

                    map = new SqlBulkCopyColumnMapping(table.Columns["EntityId"].ColumnName, "EntityId");
                    bulkCopy.ColumnMappings.Add(map);

                    map = new SqlBulkCopyColumnMapping(table.Columns["EntityType"].ColumnName, "EntityType");
                    bulkCopy.ColumnMappings.Add(map);

                    map = new SqlBulkCopyColumnMapping(table.Columns["CustomerId"].ColumnName, "CustomerId");
                    bulkCopy.ColumnMappings.Add(map);

                    map = new SqlBulkCopyColumnMapping(table.Columns["OperationType"].ColumnName, "OperationType");
                    bulkCopy.ColumnMappings.Add(map);

                    map = new SqlBulkCopyColumnMapping(table.Columns["Data"].ColumnName, "Data");
                    bulkCopy.ColumnMappings.Add(map);

                    map = new SqlBulkCopyColumnMapping(table.Columns["CreatedOnUtc"].ColumnName, "CreatedOnUtc");
                    bulkCopy.ColumnMappings.Add(map);
                }

                bulkCopy.WriteToServer(table);
                con.Close();
            }
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public void ForceDelete(object entity)
        {
            try
            {
                this.Configuration.ValidateOnSaveEnabled = false;
                this.Entry(entity).State = EntityState.Deleted;
                SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        #endregion
    }
}
