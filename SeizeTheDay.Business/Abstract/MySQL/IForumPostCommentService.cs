using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Abstract.MySQL
{
    public interface IForumPostCommentService
    {
        List<ForumPostComment> GetList();
        void Add(ForumPostComment forumPostComment);
        void Delete(ForumPostComment forumPostComment);
        void Update(ForumPostComment forumPostComment);
        ForumPostComment GetByForumPostComment(int id);
        List<ForumPostComment> GetByForumPostCommentID(int id);
        List<ForumPostComment> GetByForumPostID(int id);
        ForumPostComment GetFirstOrDefaultInclude(int id);
        List<ForumPostComment> GetCommentsByPostId(int id);
        /// <summary>
        /// Gets comment list by include
        /// </summary>
        /// <returns></returns>
        List<ForumPostComment> GetListWithInclude();

    }
}
