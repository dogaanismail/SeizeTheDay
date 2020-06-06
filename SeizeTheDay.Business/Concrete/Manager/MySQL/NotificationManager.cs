using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.Core.Aspects.Postsharp.CacheAspects;
using SeizeTheDay.Core.CrossCuttingConcerns.Caching.Microsoft;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Concrete.Manager.MySQL
{
    public class NotificationManager : INotificationService
    {
        #region Fields
        private readonly INotificationDal _notificationDal;
        #endregion

        #region Ctor

        public NotificationManager(INotificationDal notificationDal)
        {
            _notificationDal = notificationDal;
        }
        #endregion

        public void Add(Notification notification)
        {
            _notificationDal.Add(notification);
        }

        public void Delete(Notification notification)
        {
            _notificationDal.Delete(notification);
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<Notification> GetByNotfIDList(int id)
        {
            return _notificationDal.Query(x => x.NotificationID == id);
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public Notification GetByNotificationID(int id)
        {
            return _notificationDal.Find(x => x.NotificationID == id);
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public Notification GetByUserID(string id)
        {
            return _notificationDal.Find(x => x.SentTo == id);
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<Notification> GetByUserIDList(string id)
        {
            return _notificationDal.Query(x => x.SentTo == id);
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<Notification> GetList()
        {
            return _notificationDal.GetList();
        }

        public void Update(Notification notification)
        {
             _notificationDal.Update(notification);
        }
    }
}
