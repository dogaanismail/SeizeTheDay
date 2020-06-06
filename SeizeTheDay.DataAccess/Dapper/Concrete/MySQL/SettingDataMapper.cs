using Dapper;
using SeizeTheDay.DataAccess.Dapper.Abstract.MySQL;
using System;
using System.Linq;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.DataAccess.Dapper.Concrete.MySQL
{
    public class SettingDataMapper : AbstractDataMapper<Setting>, ISettingDataMapper
    {
        protected override string TableName => "Setting";

        protected override string PrimaryKeyName => "SettingId";

        public Setting FindById(int id)
        {
            return FindSingle($"select * from {this.TableName} WHERE {this.PrimaryKeyName}=@Id", new { Id = id });
        }

        public void Insert(Setting item)
        {
            using (var cn = Connection)
            {
                cn.Open();
                item.SettingId =
                    cn.Query<int>(
                        $"INSERT INTO {this.TableName} ({this.InsertQuery}) OUTPUT inserted.SettingId VALUES ({this.InsertQueryParameters})",
                        InsertParam(item)).First();
            }
        }

        public override Setting Map(dynamic result)
        {
            var data = new Setting
            {
                SettingId = result.SettingId,
                Name = result.Name,
                Value = result.Value
            };

            return data;
        }

        public void Update(Setting item)
        {
            throw new NotImplementedException();
        }

        private static object InsertParam(Setting item)
        {
            return new
            {
                item.SettingId,
                item.Name,
                item.Value
            };
        }

        public Setting GetByName(string name)
        {
            return FindSingle($"select * from {this.TableName} WHERE Name = '" + name + "'", null);
        }

        private string InsertQuery => "SettingId, " +
                                        "Name, " +
                                        "Value ";

        private string InsertQueryParameters => "@SettingId, " +
                                        "@Name, " +
                                        "@Value ";
    }
}
