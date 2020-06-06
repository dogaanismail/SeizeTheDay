using System.Collections.Generic;
using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.Core.Aspects.Postsharp.CacheAspects;
using SeizeTheDay.Core.CrossCuttingConcerns.Caching.Microsoft;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Concrete.Manager.MySQL
{
    public class PortalMessageManager : IPortalMessagesService
    {
        #region Fields
        private IPortalMessagesDal _portalMessagesDal;
        #endregion

        #region Ctor
        public PortalMessageManager(IPortalMessagesDal portalMessagesDal)
        {
            _portalMessagesDal = portalMessagesDal;
        }
        #endregion

        public void Add(PortalMessage messages)
        {
            _portalMessagesDal.Add(messages);
        }

        public void Delete(PortalMessage messages)
        {
            _portalMessagesDal.Delete(messages);
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public PortalMessage FirstOrDefault()
        {
            return _portalMessagesDal.FirstOrDefault();
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<PortalMessage> GetAllLazyWithoutID()
        {
            return _portalMessagesDal.TolistInclude(x => x.User, x=>x.User.UserInfoe_Id);
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public PortalMessage GetByMessageID(int id)
        {
            return _portalMessagesDal.Find(x => x.MessageID == id);
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<PortalMessage> GetByMessageIDToList(int id)
        {
            return _portalMessagesDal.Query(x => x.MessageID == id);
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public PortalMessage GetByUserID(string id)
        {
            return _portalMessagesDal.Find(x => x.PortalMessageUserID == id);
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<PortalMessage> GetByUserIDToList(string id)
        {
            return _portalMessagesDal.Query(x => x.PortalMessageUserID == id);
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<PortalMessage> GetList()
        {
            return _portalMessagesDal.GetList();
        }

        public void Update(PortalMessage messages)
        {
             _portalMessagesDal.Update(messages);
        }
    }
}
