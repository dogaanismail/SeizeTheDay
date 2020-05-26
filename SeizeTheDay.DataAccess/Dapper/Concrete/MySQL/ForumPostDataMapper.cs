using Dapper;
using SeizeTheDay.DataAccess.Dapper.Abstract.MySQL;
using SeizeTheDay.DataDomain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.DataAccess.Dapper.Concrete.MySQL
{
    public class ForumPostDataMapper : AbstractDataMapper<ForumPost>, IForumPostDataMapper
    {
        protected override string TableName => "ForumPost";

        protected override string PrimaryKeyName => "ForumPostID";

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
            return FindSingle($"select * from {this.TableName} WHERE {this.PrimaryKeyName}=@Id", new { Id = id });
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

        public TopicDetailDto GetPostDetailById(int id)
        {
            using (var cn = Connection)
            {
                cn.Open();
                return cn.Query<TopicDetailDto>($"select post.ForumPostID, post.ForumPostTitle, post.ForumPostContent, post.CreatedTime, post.CreatedBy, topic.ForumTopicID, " +
                    $"forum.ForumID, post.ShowInPortal, post.PostLocked, post.ReviewCount, post.IsDefault,users.UserName as CreatedByUserName, " +
                    $"users.Id CreatedByUserID, info.PhotoPath CreatedByPhotoPath, forum.ForumName, topic.ForumTopicName, " +
                    $"COUNT(DISTINCT(comm.ForumPostID)) as CommentCount, COUNT(DISTINCT(postLike.postID)) as PostLikesCount " +
                    $"from {this.TableName} as post " +
                    $"INNER JOIN ForumTopic as topic ON post.ForumTopicID = post.ForumTopicID " +
                    $"INNER JOIN Forum as forum ON forum.ForumID = post.ForumID " +
                    $"INNER JOIN Users as users ON users.Id = post.CreatedBy " +
                    $"INNER JOIN UserInfoes as info ON info.Id = users.Id " +
                    $"LEFT JOIN ForumPostLike as postLike ON postLike.postID = post.ForumPostID " +
                    $"LEFT JOIN ForumPostComment as comm ON comm.ForumPostID = post.ForumPostID WHERE post.ForumPostID = {id}").FirstOrDefault();
            }
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

