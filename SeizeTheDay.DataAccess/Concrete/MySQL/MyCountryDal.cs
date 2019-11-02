using SeizeTheDay.Core.Concrete.MySQL;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.DataAccess.Concrete.MySQL
{
    public  class MyCountryDal : MyEntityRepositoryBase<Country, Xgteamc1XgTeamEntities>, ICountryDal
    {

    }
}
