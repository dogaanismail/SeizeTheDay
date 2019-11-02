using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Abstract.MySQL
{
    public interface IForumPostLikeService
    {
        List<ForumPostLike> GetList();
        void Add(ForumPostLike forumpostLike);
        void Delete(ForumPostLike forumPostLike);
        void Update(ForumPostLike forumPostLike);
        ForumPostLike GetByForumPostLike(int id);
        List<ForumPostLike> GetByForumPostLikeID(int id);
        ForumPostLike GetByUserandPostID(string userID, int postID);
    }
}
