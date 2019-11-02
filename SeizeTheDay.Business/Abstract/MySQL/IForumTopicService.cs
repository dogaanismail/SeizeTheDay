using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Abstract.MySQL
{
    public interface IForumTopicService
    {
        List<ForumTopic> GetList();
        void Add(ForumTopic forumtopic);
        void Delete(ForumTopic forumtopic);
        void Update(ForumTopic forumtopic);
        ForumTopic GetByForumTopic(int id);
        List<ForumTopic> GetByForumTopicID(int id);
        ForumTopic FirstOrDefaultInclude(int id);
        List<ForumTopic> TolistIncludeWithoutID();
        List<ForumTopic> TolistIncludeByForumID(int id);
        List<ForumTopic> StringIncludeWithExpression(int id,params string[] children);
        List<ForumTopic> StringIncludeWithoutExpression(params string[] children);
        ForumTopic SingleStringIncludeWithExp(int id, params string[] children);

    }
}
