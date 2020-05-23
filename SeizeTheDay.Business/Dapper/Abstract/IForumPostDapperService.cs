using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Dapper.Abstract
{
    public interface IForumPostDapperService
    {
        IEnumerable<ForumPost> GetForumPosts();
        void Insert(ForumPost data);
        ForumPost GetForumPost(int forumPostId);
    }
}
