using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Dapper.Abstract.MySQL
{
    public interface IForumTopicDapperService
    {
        IEnumerable<ForumTopic> GetForumTopics();
        void Insert(ForumTopic data);
        ForumTopic GetForumTopicById(int topicId);
        void Delete(int topicId);
    }
}
