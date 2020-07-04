using Dapper;
using SeizeTheDay.DataAccess.Dapper.Abstract.MySQL;
using System;
using System.Linq;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.DataAccess.Dapper.Concrete.MySQL
{
    public class ModuleDataMapper : AbstractDataMapper<Module>, IModuleDataMapper
    {
        protected override string TableName => "Modules";

        protected override string PrimaryKeyName => "ID";

        public Module FindById(int id)
        {
            return FindSingle($"select * from {this.TableName} WHERE {this.PrimaryKeyName}={id}", new { Id = id });
        }

        public void Insert(Module item)
        {
            using (var cn = Connection)
            {
                cn.Open();
                item.ID =
                    cn.Query<int>(
                        $"INSERT INTO {this.TableName} ({this.InsertQuery}) OUTPUT inserted.ID VALUES ({this.InsertQueryParameters})",
                        InsertParam(item)).First();
            }
        }

        public override Module Map(dynamic result)
        {
            var data = new Module
            {
                ID = result.LikeID,
                DisplayOrder = result.DisplayOrder,
                ModuleName = result.ModuleName,
                PageIcon = result.PageIcon,
                PageUrl = result.PageUrl,
                PageSlug = result.PageSlug,
                IsDeleted = result.IsDeleted,
                IsActive = result.IsActive,
                IsDefault = result.IsDefault,
                ParentModuleID = result.ParentModuleID
            };

            return data;
        }

        public void Update(Module item)
        {
            throw new NotImplementedException();
        }

        private static object InsertParam(Module item)
        {
            return new
            {
                item.ID,
                item.DisplayOrder,
                item.ModuleName,
                item.PageIcon,
                item.PageUrl,
                item.PageSlug,
                item.IsDeleted,
                item.IsActive,
                item.IsDefault,
                item.ParentModuleID
            };
        }

        private string InsertQuery => "ID, " +
                                        "DisplayOrder, " +
                                        "ModuleName, " +
                                        "PageIcon, " +
                                        "PageUrl, " +
                                        "PageSlug, " +
                                        "IsDeleted, " +
                                        "IsActive, " +
                                        "IsDefault, " +
                                        "ParentModuleID ";

        private string InsertQueryParameters => "@ID, " +
                                        "@DisplayOrder, " +
                                        "@ModuleName, " +
                                        "@PageIcon, " +
                                        "@PageUrl, " +
                                        "@PageSlug, " +
                                        "@IsDeleted, " +
                                        "@IsActive, " +
                                        "@IsDefault, " +
                                        "@ParentModuleID";
    }
}
