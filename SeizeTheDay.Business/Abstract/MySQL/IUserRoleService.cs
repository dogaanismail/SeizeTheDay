using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Abstract.MySQL
{
    public interface IUserRoleService
    {
        UserRole GetByUserID(string userID);
        void Update(UserRole userRole);
        void Add(UserRole userRole);
        void Delete(UserRole userRole);
        List<UserRole> GetList();
        List<UserRole> GetByUserIDList(string userID);
    }
}
