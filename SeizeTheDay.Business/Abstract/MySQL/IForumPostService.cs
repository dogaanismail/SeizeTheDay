using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Abstract.MySQL
{
    public interface IForumPostService
    {
        List<ForumPost> GetList();
        void Add(ForumPost forumpost);
        void Delete(ForumPost forumpost);
        void Update(ForumPost forumpost);
        ForumPost GetByForumPost(int id);
        List<ForumPost> GetByForumPostID(int id);
        ForumPost GetFirstOrDefaultInclude(int id);
        ForumPost GetFirstOrDefaultInclude2(int id);
        List<ForumPost> GetAllLazy(int id);
        List<ForumPost> GetByForumTopicID(int id);
        List<ForumPost> GetAllLazyWithoutID();

        List<ForumPost> StringIncludeWithExp(int id, params string[] children);
        List<ForumPost> IncludeWithoutExp();

        ForumPost SingleStringIncludeWithExp(int id, params string[] children);

        ForumPost SingleInclude(int id, params string[] children);

        List<ForumPost> NewPost(params string[] children);
        List<ForumPost> MostRepliedComment(params string[] children);
    }
}
