using SeizeTheDay.Business.Dapper.Abstract.MySQL;
using SeizeTheDay.DataAccess.Dapper.Abstract.MySQL;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Dapper.Concrete.MySQL
{
    public class ForumTopicDapperService : IForumTopicDapperService
    {
        #region Ctor
        private readonly IForumTopicDataMapper _mapper;

        public ForumTopicDapperService(IForumTopicDataMapper mapper)
        {
            _mapper = mapper;
        }

        #endregion

        public void Delete(int topicId)
        {
            _mapper.Delete(topicId);
        }

        public ForumTopic GetForumTopicById(int topicId)
        {
            return _mapper.FindById(topicId);
        }

        public IEnumerable<ForumTopic> GetForumTopics()
        {
            return _mapper.FindAll();
        }

        public void Insert(ForumTopic data)
        {
            _mapper.Insert(data);
        }
    }
}
