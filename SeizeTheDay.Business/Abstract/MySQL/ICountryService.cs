using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Abstract.MySQL
{
    public interface ICountryService
    {
        List<Country> GetList();
        void Add(Country country);
        void Delete(Country country);
        void Update(Country country);
        Country GetByCountry(int id);
        List<Country> GetByCountryID(int id);
        Country FirstOrDefault();
    }
}
