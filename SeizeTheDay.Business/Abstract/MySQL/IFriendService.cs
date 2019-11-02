using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Abstract.MySQL
{
    public interface IFriendService
    {
        List<Friend> GetList();
        void Add(Friend friend);
        void Delete(Friend friend);
        void Update(Friend friend);
        Friend GetByFriendID(int id);
        List<Friend> GetByFriendIDTolist(int id);
        List<Friend> GetAllLazy();
        Friend GetByUserID(string id);
        List<Friend> GetByUserIDTolist(string id);
        Friend GetByFutureFriendID(string id);
        List<Friend> GetByFutureFriendIDTolist(string id);
        Friend GetByFirstOrDefault();
        Friend GetByUserandFuture(string userID, string futureID);
    }
}
