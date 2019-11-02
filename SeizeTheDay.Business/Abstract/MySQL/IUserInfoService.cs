using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Abstract.MySQL
{
    public interface IUserInfoService
    {
        UserInfoe GetByUserID(string userID);
        UserInfoe GetByUserName(string UserName);
        UserInfoe GetByEmail(string email);
        UserInfoe GetFirstOrDefaultInclude(string id);
        void Update(UserInfoe User);
        void AddUser(UserInfoe user);
        List<UserInfoe> GetList();
        void Delete(UserInfoe user);
    }
}
