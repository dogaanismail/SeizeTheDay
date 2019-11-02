using System.Collections.Generic;
using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Concrete.Manager.MySQL
{
    public class CountryManager : ICountryService
    {
        private ICountryDal _countryDal;

        public CountryManager(ICountryDal countryDal)
        {
            _countryDal = countryDal;
        }

        public void Add(Country country)
        {
            _countryDal.Add(country);
        }

        public void Delete(Country country)
        {
            _countryDal.Delete(country);
        }

        public Country FirstOrDefault()
        {
           return _countryDal.FirstOrDefault();
        }

        public Country GetByCountry(int id)
        {
            return _countryDal.Find(x => x.CountryID == id);
        }

        public List<Country> GetByCountryID(int id)
        {
            return _countryDal.Query(x => x.CountryID == id);
        }

        public List<Country> GetList()
        {
           return  _countryDal.GetList();
        }

        public void Update(Country country)
        {
            _countryDal.Update(country);
        }
    }
}
