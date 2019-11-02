using SeizeTheDay.Core.Concrete.MySQL;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.DataAccess.Concrete.MySQL
{
    public class MyForumDal : MyEntityRepositoryBase<Forum, Xgteamc1XgTeamEntities>, IForumDal
    {
    }
}
