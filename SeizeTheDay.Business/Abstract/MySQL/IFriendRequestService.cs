using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Abstract.MySQL
{
    public interface IFriendRequestService
    {
        List<FriendRequest> GetList();
        void Add(FriendRequest request);
        void Delete(FriendRequest request);
        void Update(FriendRequest request);
        FriendRequest GetByrequest(int id);
        List<FriendRequest> GetByrequestID(int id);
        List<FriendRequest> GetAllLazy();
        FriendRequest GetByUserID(string id);
        List<FriendRequest> GetByUserIDTolist(string id);
        FriendRequest GetByFutureFriendID(string id);
        List<FriendRequest> GetByFutureFriendIDTolist(string id);
    
    }
}
