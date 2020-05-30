using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Abstract.MySQL
{
    public interface IUserService
    {      
        User GetByUserID(string userID);
        User GetByUserName(string UserName);
        User GetByEmail(string email);
        User GetFirstOrDefaultInclude(string id);
        void Update(User User);
        void AddUser(User user);
        List<User> GetList();
        List<User> TolistInclude();
        void Delete(User user);
        List<User> StringIncludeWithoutExpression();
        User SingleStringIncludeWithExp(string id, params string[] children);
        User GetUserNotifications(string userName, params string[] children);
    }
}
