using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Dapper.Abstract.MySQL
{
    public interface IForumDapperService
    {
        IEnumerable<Forum> GetForums();
        void Insert(Forum data);
        Forum GetForumById(int forumId);
        void Delete(int forumId);
    }
}
