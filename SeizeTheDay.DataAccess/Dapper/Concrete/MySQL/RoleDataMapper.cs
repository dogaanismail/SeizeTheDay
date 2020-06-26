using Dapper;
using SeizeTheDay.DataAccess.Dapper.Abstract.MySQL;
using System;
using System.Linq;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.DataAccess.Dapper.Concrete.MySQL
{
    public class RoleDataMapper : AbstractDataMapper<Role>, IRoleDataMapper
    {
        protected override string TableName => "Roles";

        protected override string PrimaryKeyName => "Id";

        public Role FindById(int id)
        {
            return FindSingle($"select * from {this.TableName} WHERE {this.PrimaryKeyName}={id}", new { Id = id });
        }

        public void Insert(Role item)
        {
            using (var cn = Connection)
            {
                cn.Open();
                item.Id =
                    cn.Query<string>(
                        $"INSERT INTO {this.TableName} ({this.InsertQuery}) OUTPUT inserted.Id VALUES ({this.InsertQueryParameters})",
                        InsertParam(item)).First();
            }
        }

        public override Role Map(dynamic result)
        {
            var data = new Role
            {
                Id = result.Id,
                Name = result.Name,
                Discriminator = result.Discriminator
            };

            return data;
        }

        public void Update(Role item)
        {
            throw new NotImplementedException();
        }

        private static object InsertParam(Role item)
        {
            return new
            {
                item.Id,
                item.Name,
                item.Discriminator
            };
        }

        private string InsertQuery => "Id, " +
                                        "Name, " +
                                        "Discriminator ";

        private string InsertQueryParameters => "@Id, " +
                                        "@Name, " +
                                        "@Discriminator ";
    }
}
