using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Abstract.MySQL
{
    public interface IForumService
    {
        List<Forum> GetList();
        void Add(Forum forum);
        void Delete(Forum forum);
        void Update(Forum forum);
        Forum GetByForum(int id);
        List<Forum> GetByForumID(int id);
        List<Forum> GetAllLazy();
    }
}
