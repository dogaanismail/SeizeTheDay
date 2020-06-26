using Dapper;
using SeizeTheDay.DataAccess.Dapper.Abstract.MySQL;
using System;
using System.Linq;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.DataAccess.Dapper.Concrete.MySQL
{
    public class UserRoleDataMapper : AbstractDataMapper<UserRole>, IUserRoleDataMapper
    {
        protected override string TableName => "UserRoles";

        protected override string PrimaryKeyName => "UserId";

        protected string PrimaryKeyRole => "RoleId";

        public UserRole FindById(int id)
        {
            return FindSingle($"select * from {this.TableName} WHERE {this.PrimaryKeyName}={id}", new { Id = id });
        }

        public UserRole FindByRoleId(string id)
        {
            return FindSingle($"select * from {this.TableName} WHERE {this.PrimaryKeyName}={id}", new { Id = id });
        }

        public void Insert(UserRole item)
        {
            using (var cn = Connection)
            {
                cn.Open();
                item.UserId =
                    cn.Query<string>(
                        $"INSERT INTO {this.TableName} ({this.InsertQuery}) OUTPUT inserted.UserId VALUES ({this.InsertQueryParameters})",
                        InsertParam(item)).First();
            }
        }

        public override UserRole Map(dynamic result)
        {
            var data = new UserRole
            {
                UserId = result.UserId,
                RoleId = result.RoleId,
                IdentityUserId = result.IdentityUserId,
                Discriminator = result.Discriminator
            };

            return data;
        }

        public void Update(UserRole item)
        {
            throw new NotImplementedException();
        }

        private static object InsertParam(UserRole item)
        {
            return new
            {
                item.UserId,
                item.RoleId,
                item.IdentityUserId,
                item.Discriminator
            };
        }

        private string InsertQuery => "UserId, " +
                                        "RoleId, " +
                                        "IdentityUserId, " +
                                        "Discriminator ";

        private string InsertQueryParameters => "@UserId, " +
                                        "@RoleId, " +
                                        "@IdentityUserId, " +
                                        "@Discriminator";
    }
}
