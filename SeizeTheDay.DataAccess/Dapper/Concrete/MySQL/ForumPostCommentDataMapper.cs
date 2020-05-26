using Dapper;
using SeizeTheDay.DataAccess.Dapper.Abstract.MySQL;
using System;
using System.Linq;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.DataAccess.Dapper.Concrete.MySQL
{
    public class ForumPostCommentDataMapper : AbstractDataMapper<ForumPostComment>, IForumPostCommentDataMapper
    {
        protected override string TableName => "ForumPostComment";

        protected override string PrimaryKeyName => "ForumPostCommentID";

        public ForumPostComment FindById(int id)
        {
            return FindSingle($"select * from {this.TableName} WHERE {this.PrimaryKeyName}=@Id", new { Id = id });
        }

        public void Insert(ForumPostComment item)
        {
            using (var cn = Connection)
            {
                cn.Open();
                item.ForumPostCommentID =
                    cn.Query<int>(
                        $"INSERT INTO {this.TableName} ({this.InsertQuery}) OUTPUT inserted.ForumPostCommentID VALUES ({this.InsertQueryParameters})",
                        InsertParam(item)).First();
            }
        }

        public override ForumPostComment Map(dynamic result)
        {
            var data = new ForumPostComment
            {
                ForumPostCommentID = result.ForumPostCommentID,
                Text = result.Text,
                CreatedTime = result.CreatedTime,
                CreatedBy = result.CreatedBy,
                ForumPostID = result.ForumPostID
            };

            return data;
        }

        public void Update(ForumPostComment item)
        {
            throw new NotImplementedException();
        }

        private static object InsertParam(ForumPostComment item)
        {
            return new
            {
                item.ForumPostCommentID,
                item.Text,
                item.CreatedTime,
                item.CreatedBy,
                item.ForumPostID
            };
        }

        public ForumPostComment GetByPostId(int id)
        {
            return FindSingle($"select * from {this.TableName} WHERE ForumPostID=@Id", new { Id = id });
        }

        private string InsertQuery => "ForumPostCommentID, " +
                                        "Text, " +
                                        "ForumPostContent, " +
                                        "CreatedTime, " +
                                        "CreatedBy, " +
                                        "ForumPostID";

        private string InsertQueryParameters => "@ForumPostCommentID, " +
                                        "@Text, " +
                                        "@CreatedTime, " +
                                        "@CreatedBy, " +
                                        "@ForumPostID";
    }
}
