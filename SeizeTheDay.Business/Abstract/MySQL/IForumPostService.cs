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
        ForumPost GetFirstOrDefaultInclude(int id);
        List<ForumPost> GetByForumTopicID(int id);
        List<ForumPost> GetAllLazyWithoutID();
        ForumPost SingleInclude(int id, params string[] children);
    }
}
