using Dapper;
using SeizeTheDay.DataAccess.Dapper.Abstract.MySQL;
using System;
using System.Linq;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.DataAccess.Dapper.Concrete.MySQL
{
    public class ForumDataMapper : AbstractDataMapper<Forum>, IForumDataMapper
    {
        protected override string TableName => "Forum";

        protected override string PrimaryKeyName => "ForumID";

        public override Forum Map(dynamic result)
        {
            var data = new Forum
            {
                ForumID = result.ForumPostID,
                ForumName = result.ForumPostTitle,
                Title = result.ForumPostContent,
                Description = result.Description,
                CreatedTime = result.CreatedTime,
                Status = result.Status,
                CreatedBy = result.CreatedBy,
                IsDefault = result.IsDefault
            };

            return data;
        }

        public Forum FindById(int id)
        {
            return FindSingle($"select * from {this.TableName} WHERE {this.PrimaryKeyName}=@Id", new { Id = id });
        }

        public void Insert(Forum item)
        {
            using (var cn = Connection)
            {
                cn.Open();
                item.ForumID =
                    cn.Query<int>(
                        $"INSERT INTO {this.TableName} ({this.InsertQuery}) OUTPUT inserted.ForumID VALUES ({this.InsertQueryParameters})",
                        InsertParam(item)).First();
            }
        }

        public void Update(Forum item)
        {
            throw new NotImplementedException();
        }

        private static object InsertParam(Forum item)
        {
            return new
            {
                item.ForumID,
                item.ForumName,
                item.Title,
                item.Description,
                item.CreatedTime,
                item.Status,
                item.CreatedBy,
                item.IsDefault
            };
        }

        private string InsertQuery => "ForumID, " +
                                        "ForumName, " +
                                        "Title, " +
                                        "Description, " +
                                        "CreatedTime, " +
                                        "Status, " +
                                        "CreatedBy, " +
                                        "IsDefault";

        private string InsertQueryParameters => "@ForumID, " +
                                        "@ForumName, " +
                                        "@Title, " +
                                        "@Description, " +
                                        "@CreatedTime, " +
                                        "@Status, " +
                                        "@CreatedBy, " +
                                        "@IsDefault ";

    }
}
