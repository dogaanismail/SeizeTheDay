using Microsoft.AspNet.Identity.EntityFramework;
using SeizeTheDay.Core.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeizeTheDay.Data.Initializers
{
    public abstract class SqlCeInitializer<T> : IDatabaseInitializer<T> where T : IdentityDbContext<AppUser, AppRole, int,
        AppUserLogin, AppUserRole, AppUserClaim>
    {
        public abstract void InitializeDatabase(T context);

        #region Helpers

        /// <summary>
        /// Returns a new DbContext with the same SqlCe connection string, but with the |DataDirectory| expanded
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected static IdentityDbContext<AppUser, AppRole, int,
        AppUserLogin, AppUserRole, AppUserClaim> ReplaceSqlCeConnection(IdentityDbContext<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim> context)
        {
            if (context.Database.Connection is SqlCeConnection)
            {
                var builder = new SqlCeConnectionStringBuilder(context.Database.Connection.ConnectionString);
                if (!String.IsNullOrWhiteSpace(builder.DataSource))
                {
                    builder.DataSource = ReplaceDataDirectory(builder.DataSource);
                    return new IdentityDbContext<AppUser, AppRole, int,
        AppUserLogin, AppUserRole, AppUserClaim>(builder.ConnectionString);
                }
            }
            return context;
        }

        private static string ReplaceDataDirectory(string inputString)
        {
            string str = inputString.Trim();
            if (string.IsNullOrEmpty(inputString) || !inputString.StartsWith("|DataDirectory|", StringComparison.InvariantCultureIgnoreCase))
            {
                return str;
            }
            var data = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
            if (string.IsNullOrEmpty(data))
            {
                data = AppDomain.CurrentDomain.BaseDirectory ?? Environment.CurrentDirectory;
            }
            if (string.IsNullOrEmpty(data))
            {
                data = string.Empty;
            }
            int length = "|DataDirectory|".Length;
            if ((inputString.Length > "|DataDirectory|".Length) && ('\\' == inputString["|DataDirectory|".Length]))
            {
                length++;
            }
            return Path.Combine(data, inputString.Substring(length));
        }

        #endregion
    }
}
