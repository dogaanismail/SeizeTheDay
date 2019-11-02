using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Concrete.Manager.MySQL
{
    public class UserRoleManager : IUserRoleService
    {
        private IUserRoleDal _userRoleDal;
        public UserRoleManager(IUserRoleDal userRoleDal)
        {
            _userRoleDal = userRoleDal;
        }

        public void Add(UserRole userRole)
        {
            _userRoleDal.Add(userRole);
        }

        public void Delete(UserRole userRole)
        {
            _userRoleDal.Delete(userRole);
        }

        public UserRole GetByUserID(string userID)
        {
            return _userRoleDal.Find(x => x.UserId == userID);
        }

        public List<UserRole> GetByUserIDList(string userID)
        {
            return _userRoleDal.Query(x => x.UserId == userID);
        }

        public List<UserRole> GetList()
        {
            return _userRoleDal.GetList() ;
        }

        public void Update(UserRole userRole)
        {
            _userRoleDal.Update(userRole);
        }
    }
}
