using System.Collections.Generic;
using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.Core.CrossCuttingConcerns.Caching.Microsoft;
using SeizeTheDay.Core.Aspects.Postsharp.CacheAspects;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Concrete.Manager.MySQL
{
    public class UserTypeManager : IUserTypeService
    {
        #region Fields
        private IUserTypeDal _userTypeDal;
        #endregion

        #region Ctor
        public UserTypeManager(IUserTypeDal userTypeDal)
        {
            _userTypeDal = userTypeDal;
        }
        #endregion

        public void Add(UserType userType)
        {
            _userTypeDal.Add(userType);
        }

        public void Delete(UserType userType)
        {
            _userTypeDal.Delete(userType);
        }

        public UserType GetByUserTypeID(int id)
        {
            return _userTypeDal.Find(x => x.UserTypeID == id);
        }

        public List<UserType> GetByUserTypeIDList(int id)
        {
            return _userTypeDal.Query(x => x.UserTypeID == id);
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<UserType> GetList()
        {
            return _userTypeDal.GetList();
        }

        //Error EntityCollection
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<UserType> GetListWithInclude()
        {
            //return _userTypeDal.TolistInclude(x => x.UserInfoes);
            return _userTypeDal.StringInclude("UserInfoes", "UserInfoes.User_Id");
        }

        public void Update(UserType userType)
        {
            _userTypeDal.Update(userType);
        }
    }
}
