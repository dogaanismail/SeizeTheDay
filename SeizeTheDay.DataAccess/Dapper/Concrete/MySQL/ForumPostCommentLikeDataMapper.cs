using Dapper;
using SeizeTheDay.DataAccess.Dapper.Abstract.MySQL;
using System;
using System.Linq;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.DataAccess.Dapper.Concrete.MySQL
{
    public class ForumPostCommentLikeDataMapper : AbstractDataMapper<ForumCommentLike>, IForumPostCommentLikeDataMapper
    {
        protected override string TableName => "ForumCommentLike";

        protected override string PrimaryKeyName => "commentLikeID";

        public ForumCommentLike FindById(int id)
        {
            return FindSingle($"select * from {this.TableName} WHERE {this.PrimaryKeyName}=@Id", new { Id = id });
        }

        public void Insert(ForumCommentLike item)
        {
            using (var cn = Connection)
            {
                cn.Open();
                item.CommentLikeID =
                    cn.Query<int>(
                        $"INSERT INTO {this.TableName} ({this.InsertQuery}) OUTPUT inserted.commentLikeID VALUES ({this.InsertQueryParameters})",
                        InsertParam(item)).First();
            }
        }

        public override ForumCommentLike Map(dynamic result)
        {
            var data = new ForumCommentLike
            {
                CommentLikeID = result.LikeID,
                UserID = result.UserID,
                CommentID = result.PostID,
                LikedDate = result.LikedDate,
            };

            return data;
        }

        public void Update(ForumCommentLike item)
        {
            throw new NotImplementedException();
        }

        private static object InsertParam(ForumCommentLike item)
        {
            return new
            {
                item.CommentLikeID,
                item.UserID,
                item.CommentID,
                item.LikedDate
            };
        }

        private string InsertQuery => "CommentLikeID, " +
                                        "UserID, " +
                                        "CommentID, " +
                                        "LikedDate ";

        private string InsertQueryParameters => "@CommentLikeID, " +
                                        "@UserID, " +
                                        "@CommentID, " +
                                        "@LikedDate";
    }
}
