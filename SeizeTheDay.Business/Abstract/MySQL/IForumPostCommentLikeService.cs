using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Abstract.MySQL
{
    public interface IForumPostCommentLikeService
    {
        List<ForumCommentLike> GetList();
        void Add(ForumCommentLike forumCommentLike);
        void Delete(ForumCommentLike forumCommentLike);
        void Update(ForumCommentLike forumCommentLike);
        ForumCommentLike GetByForumCommentLike(int id);
        List<ForumCommentLike> GetByForumCommentLikeID(int id);
        ForumCommentLike GetByCommentandUserID(string userID, int commentID);
        ForumCommentLike GetByComment(int id);
        List<ForumCommentLike> GetByForumCommentIDTolist(int id);
    }
}
