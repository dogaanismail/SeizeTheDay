using Dapper;
using Devart.Data.MySql;
using SeizeTheDay.Common.Helpers;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;

namespace SeizeTheDay.DataAccess.Dapper.Concrete
{
    /// <summary>
    /// The abstract data mapper.
    /// </summary>
    /// <typeparam name="T">
    /// The data entity.
    /// </typeparam>
    public abstract class AbstractDataMapper<T>
    {
        /// <summary>
        /// Gets the table name.
        /// </summary>
        protected abstract string TableName { get; }

        /// <summary>
        /// Gets the database connection. There is a MySQL connection here.
        /// </summary>
        protected IDbConnection Connection
        {
            get
            {
                return
                    new MySqlConnection(ConfigurationManager.ConnectionStrings["IdentityConnection"].ConnectionString.ToString());
            }
        }

        /// <summary>System.ArgumentException: 'Anahtar sözcük desteklenmiyor: 'host'.'
        /// Finds a single record.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <param name="param">
        /// The parameters.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public virtual T FindSingle(string query, dynamic param)
        {
            dynamic item = null;

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var result = cn.Query(query, (object)param).SingleOrDefault();

                if (result != null)
                {
                    item = Map(result);
                }
            }

            return item;
        }

        /// <summary>
        /// Finds and returns all data.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public virtual IEnumerable<T> FindAll()
        {
            var items = new List<T>();

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                var results = cn.Query("SELECT * FROM " + TableName).ToList();

                for (int i = 0; i < results.Count(); i++)
                {
                    items.Add(Map(results.ElementAt(i)));
                }
            }

            return items;
        }

        /// <summary>
        /// Deletes a data.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        public virtual void Delete(int id)
        {
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Execute("DELETE FROM " + TableName + " WHERE ID=@ID", new { ID = id });
            }
        }

        /// <summary>
        /// Maps a data.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <returns>
        /// The <see cref="dynamic"/>.
        /// </returns>
        public abstract T Map(dynamic result);
    }
}
