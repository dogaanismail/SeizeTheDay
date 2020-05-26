using Dapper;
using SeizeTheDay.DataAccess.Dapper.Abstract.MySQL;
using System;
using System.Linq;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.DataAccess.Dapper.Concrete.MySQL
{
    public class ForumPostLikeDataMapper : AbstractDataMapper<ForumPostLike>, IForumPostLikeDataMapper
    {
        protected override string TableName => "ForumPostLike";

        protected override string PrimaryKeyName => "LikeID";

        public ForumPostLike FindById(int id)
        {
            return FindSingle($"select * from {this.TableName} WHERE {this.PrimaryKeyName}=@Id", new { Id = id });
        }

        public void Insert(ForumPostLike item)
        {
            using (var cn = Connection)
            {
                cn.Open();
                item.LikeID =
                    cn.Query<int>(
                        $"INSERT INTO {this.TableName} ({this.InsertQuery}) OUTPUT inserted.LikeID VALUES ({this.InsertQueryParameters})",
                        InsertParam(item)).First();
            }
        }

        public override ForumPostLike Map(dynamic result)
        {
            var data = new ForumPostLike
            {
                LikeID = result.LikeID,
                UserID = result.UserID,
                PostID = result.PostID,
                LikedDate = result.LikedDate,
            };

            return data;
        }

        public void Update(ForumPostLike item)
        {
            throw new NotImplementedException();
        }

        private static object InsertParam(ForumPostLike item)
        {
            return new
            {
                item.LikeID,
                item.UserID,
                item.PostID,
                item.LikedDate
            };
        }

        private string InsertQuery => "LikeID, " +
                                        "UserID, " +
                                        "PostID, " +
                                        "LikedDate ";

        private string InsertQueryParameters => "@LikeID, " +
                                        "@UserID, " +
                                        "@PostID, " +
                                        "@LikedDate";

    }
}
