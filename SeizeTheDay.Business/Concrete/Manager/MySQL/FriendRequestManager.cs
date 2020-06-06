using System.Collections.Generic;
using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Concrete.Manager.MySQL
{
    public class FriendRequestManager : IFriendRequestService
    {
        #region Fields
        private IFriendRequestDal _friendRequestDal;
        #endregion

        #region Ctor
        public FriendRequestManager(IFriendRequestDal friendRequestDal)
        {
            _friendRequestDal = friendRequestDal;
        }
        #endregion

        public void Add(FriendRequest request)
        {
            _friendRequestDal.Add(request);
        }

        public void Delete(FriendRequest request)
        {
            _friendRequestDal.Delete(request);
        }

        public List<FriendRequest> GetAllLazy()
        {
            throw new System.NotImplementedException();
        }

        public FriendRequest GetByFutureFriendID(string id)
        {
            return _friendRequestDal.Find(x => x.FutureFriendID == id);
        }

        public List<FriendRequest> GetByFutureFriendIDTolist(string id)
        {
            return _friendRequestDal.Query(x => x.FutureFriendID == id);
        }

        public FriendRequest GetByrequest(int id)
        {
            return _friendRequestDal.Find(x => x.FriendRequestID == id);
        }

        public List<FriendRequest> GetByrequestID(int id)
        {
            return _friendRequestDal.Query(x => x.FriendRequestID == id);
        }

        public FriendRequest GetByUserID(string id)
        {
            return _friendRequestDal.Find(x => x.UserID == id);
        }

        public List<FriendRequest> GetByUserIDTolist(string id)
        {
            return _friendRequestDal.Query(x => x.UserID == id);
        }

        public List<FriendRequest> GetList()
        {
            return _friendRequestDal.GetList();
        }

        public void Update(FriendRequest request)
        {
             _friendRequestDal.Update(request);
        }
    }
}
