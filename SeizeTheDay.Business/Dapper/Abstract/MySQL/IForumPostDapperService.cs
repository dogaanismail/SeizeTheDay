using SeizeTheDay.DataDomain.DTOs;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Dapper.Abstract.MySQL
{
    public interface IForumPostDapperService
    {
        /// <summary>
        /// Returns a forum posts list without include child entities.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ForumPost> GetForumPosts();

        /// <summary>
        /// Inserts a post.
        /// </summary>
        /// <param name="data"></param>
        void Insert(ForumPost data);

        /// <summary>
        /// Returns a forum posts list without include child entities.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TopicDetailDto> GetPosts();

        /// <summary>
        /// Returns a post by postId.
        /// </summary>
        /// <param name="forumPostId"></param>
        /// <returns></returns>
        ForumPost GetForumPost(int forumPostId);

        /// <summary>
        /// Deletes a post by postId
        /// </summary>
        /// <param name="forumPostId"></param>
        void Delete(int forumPostId);

        /// <summary>
        ///  Returns post detail by post id.
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        TopicDetailDto GetPostDetailById(int postId);
    }
}
