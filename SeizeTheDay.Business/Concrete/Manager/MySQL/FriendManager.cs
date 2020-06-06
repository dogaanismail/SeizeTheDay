using System;
using System.Collections.Generic;
using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Concrete.Manager.MySQL
{
    public class FriendManager: IFriendService
    {
        #region Fields
        private IFriendDal _friendDal;
        #endregion

        #region Ctor
        public FriendManager(IFriendDal friendDal)
        {
            _friendDal = friendDal;
        }
        #endregion

        public void Add(Friend friend)
        {
            _friendDal.Add(friend);
        }

        public void Delete(Friend friend)
        {
            _friendDal.Delete(friend);
        }

        public List<Friend> GetAllLazy()
        {
            throw new NotImplementedException();
        }

        public Friend GetByFirstOrDefault()
        {
            return _friendDal.FirstOrDefault();
        }

        public Friend GetByFriendID(int id)
        {
            return _friendDal.Find(x => x.FriendID == id);
        }

        public List<Friend> GetByFriendIDTolist(int id)
        {
            return _friendDal.Query(x => x.FriendID == id);
        }

        public Friend GetByFutureFriendID(string id)
        {
            return _friendDal.Find(x => x.FutureFriendID == id);
        }

        public List<Friend> GetByFutureFriendIDTolist(string id)
        {
            return _friendDal.Query(x => x.FutureFriendID == id);
        }

        public Friend GetByUserandFuture(string userID, string futureID)
        {
            return _friendDal.Find(x => x.UserID == userID && x.FutureFriendID == futureID);
        }

        public Friend GetByUserID(string id)
        {
            return _friendDal.Find(x => x.UserID == id);
        }

        public List<Friend> GetByUserIDTolist(string id)
        {
            return _friendDal.Query(x => x.UserID == id);
        }

        public List<Friend> GetList()
        {
            return _friendDal.GetList();
        }

        public void Update(Friend friend)
        {
            _friendDal.Update(friend);
        }
    }
}
