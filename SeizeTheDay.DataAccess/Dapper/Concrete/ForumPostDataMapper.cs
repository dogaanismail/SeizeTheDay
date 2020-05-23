using Dapper;
using SeizeTheDay.DataAccess.Dapper.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.DataAccess.Dapper.Concrete
{
    public class ForumPostDataMapper : AbstractDataMapper<ForumPost>, IForumPostDataMapper
    {
        protected override string TableName => "ForumPost";

        public override ForumPost Map(dynamic result)
        {
            var data = new ForumPost
            {
                ForumPostID = result.ForumPostID,
                ForumPostTitle = result.ForumPostTitle,
                ForumPostContent = result.ForumPostContent,
                CreatedTime = result.CreatedTime,
                CreatedBy = result.CreatedBy,
                ForumTopicID = result.ForumTopicID,
                ForumID = result.ForumID,
                ShowInPortal = result.ShowInPortal,
                PostLocked = result.PostLocked,
                ReviewCount = result.ReviewCount,
                IsDefault = result.IsDefault
            };

            return data;
        }

        public ForumPost FindById(int id)
        {
            return FindSingle($"select * from {this.TableName} WHERE ForumPostID=@Id", new { Id = id });
        }

        public IEnumerable<ForumPost> GetForumPosts()
        {
            return FindAll();
        }

        public void Insert(ForumPost item)
        {
            using (var cn = Connection)
            {
                cn.Open();
                item.ForumPostID =
                    cn.Query<int>(
                        $"INSERT INTO {this.TableName} ({this.InsertQuery}) OUTPUT inserted.ForumPostID VALUES ({this.InsertQueryParameters})",
                        InsertParam(item)).First();
            }
        }

        public void Update(ForumPost item)
        {
            throw new NotImplementedException();
        }

        private static object InsertParam(ForumPost item)
        {
            return new
            {
                item.ForumPostID,
                item.ForumPostTitle,
                item.ForumPostContent,
                item.CreatedTime,
                item.CreatedBy,
                item.ForumTopicID,
                item.ForumID,
                item.ShowInPortal,
                item.PostLocked,
                item.ReviewCount,
                item.IsDefault
            };
        }

        private string InsertQuery => "ForumPostID, " +
                                        "ForumPostTitle, " +
                                        "ForumPostContent, " +
                                        "CreatedTime, " +
                                        "CreatedBy, " +
                                        "ForumTopicID, " +
                                        "ForumID, " +
                                        "ShowInPortal, " +
                                        "PostLocked, " +
                                        "ReviewCount, " +
                                        "IsDefault";

        private string InsertQueryParameters => "@ForumPostID, " +
                                        "@ForumPostTitle, " +
                                        "@ForumPostContent, " +
                                        "@CreatedTime, " +
                                        "@CreatedBy, " +
                                        "@ForumTopicID, " +
                                        "@ForumID, " +
                                        "@ShowInPortal, " +
                                        "@PostLocked, " +
                                        "@ReviewCount, " +
                                        "@IsDefault";
    }
}

