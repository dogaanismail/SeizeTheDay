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
    }
}
