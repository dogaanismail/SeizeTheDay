using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Abstract.MySQL
{
    public interface IProfileVisitorService
    {
        ProfileVisitor GetByID(int id);
        List<ProfileVisitor> GetByIDTolist(int id);

        List<ProfileVisitor> GetByUserIDList(string userID);
        ProfileVisitor GetByUserID(string userID);

        List<ProfileVisitor> GetByVisitorIDList(string visitorID);
        ProfileVisitor GetByVisitorID(string visitorID);

        ProfileVisitor GetByVisitorandUserID(string visitorID,string UserID);

        void Update(ProfileVisitor visitor);
        void Add(ProfileVisitor visitor);
        List<ProfileVisitor> GetList();
        void Delete(ProfileVisitor visitor);
    }
}
