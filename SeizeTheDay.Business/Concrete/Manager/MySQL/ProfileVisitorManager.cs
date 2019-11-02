using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using System;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Concrete.Manager.MySQL
{
    public class ProfileVisitorManager : IProfileVisitorService
    {
        private IProfileVisitorDal _visitorDal;

        public ProfileVisitorManager(IProfileVisitorDal visitorDal)
        {
            _visitorDal = visitorDal;
        }
        public void Add(ProfileVisitor visitor)
        {
            _visitorDal.Add(visitor);
        }

        public void Delete(ProfileVisitor visitor)
        {
            _visitorDal.Delete(visitor);
        }

        public ProfileVisitor GetByID(int id)
        {
            return _visitorDal.Find(x => x.Id == id);
        }

        public List<ProfileVisitor> GetByIDTolist(int id)
        {
            return _visitorDal.Query(x => x.Id == id);
        }

        public ProfileVisitor GetByUserID(string userID)
        {
            return _visitorDal.Find(x => x.UserID == userID);
        }

        public List<ProfileVisitor> GetByUserIDList(string userID)
        {
            return _visitorDal.Query(x => x.UserID == userID);
        }

        public List<ProfileVisitor> GetByVisitorIDList(string visitorID)
        {
            return _visitorDal.Query(x => x.VisitorID == visitorID);
        }

        public ProfileVisitor GetByVisitorID(string visitorID)
        {
            return _visitorDal.Find(X => X.VisitorID == visitorID);
        }

        public List<ProfileVisitor> GetList()
        {
            return _visitorDal.GetList();
        }

        public void Update(ProfileVisitor visitor)
        {
            _visitorDal.Update(visitor);
        }

        public ProfileVisitor GetByVisitorandUserID(string visitorID, string UserID)
        {
            return _visitorDal.Find(x => x.VisitorID == visitorID && x.UserID == UserID);
        }
    }
}
