using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using System;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Concrete.Manager.MySQL
{
    public class UserInfoManager : IUserInfoService
    {
        #region Fields
        private IUserInfoDal _userInfoDal;
        #endregion

        #region Ctor
        public UserInfoManager(IUserInfoDal userInfoDal)
        {
            _userInfoDal = userInfoDal;
        }
        #endregion

        public void AddUser(UserInfoe user)
        {
            _userInfoDal.Add(user);
        }

        public void Delete(UserInfoe user)
        {
            _userInfoDal.Delete(user);
        }

        public UserInfoe GetByEmail(string email)
        {
            return _userInfoDal.Find(x => x.User_Id.Email == email);
        }

        public UserInfoe GetByUserID(string userID)
        {
            return _userInfoDal.Find(x => x.Id == userID);
        }

        public UserInfoe GetByUserName(string UserName)
        {
            return _userInfoDal.Find(x => x.User_Id.UserName == UserName);
        }

        public UserInfoe GetFirstOrDefaultInclude(string id)
        {
            return _userInfoDal.Find(x => x.Id == id);

        }

        public List<UserInfoe> GetList()
        {
           return  _userInfoDal.GetList();
        }

        public void Update(UserInfoe User)
        {
            _userInfoDal.Update(User);
        }
    }
}
