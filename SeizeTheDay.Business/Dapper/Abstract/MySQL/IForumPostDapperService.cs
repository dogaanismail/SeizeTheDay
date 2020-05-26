using SeizeTheDay.DataDomain.DTOs;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Dapper.Abstract.MySQL
{
    public interface IForumPostDapperService
    {
        IEnumerable<ForumPost> GetForumPosts();
        void Insert(ForumPost data);
        ForumPost GetForumPost(int forumPostId);
        void Delete(int forumPostId);

        /// <summary>
        ///  Returns post detail by post id.
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        TopicDetailDto GetPostDetailById(int postId);
    }
}
