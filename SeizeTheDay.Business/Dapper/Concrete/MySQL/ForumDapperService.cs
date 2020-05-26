using SeizeTheDay.Business.Dapper.Abstract.MySQL;
using SeizeTheDay.DataAccess.Dapper.Abstract.MySQL;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Dapper.Concrete.MySQL
{
    public class ForumDapperService : IForumDapperService
    {
        #region Ctor
        private readonly IForumDataMapper _mapper;

        public ForumDapperService(IForumDataMapper mapper)
        {
            _mapper = mapper;
        }

        #endregion

        public void Delete(int forumId)
        {
            _mapper.Delete(forumId);
        }

        public Forum GetForumById(int forumId)
        {
            return _mapper.FindById(forumId);
        }

        public IEnumerable<Forum> GetForums()
        {
            return _mapper.FindAll();
        }

        public void Insert(Forum data)
        {
            _mapper.Insert(data);
        }
    }
}
