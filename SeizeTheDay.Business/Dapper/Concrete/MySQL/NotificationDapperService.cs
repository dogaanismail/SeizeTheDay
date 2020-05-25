using SeizeTheDay.Business.Dapper.Abstract.MySQL;
using SeizeTheDay.DataAccess.Dapper.Abstract.MySQL;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Dapper.Concrete.MySQL
{
    public class NotificationDapperService : INotificationDapperService
    {
        #region Ctor
        private readonly INotificationDataMapper _mapper;

        public NotificationDapperService(INotificationDataMapper mapper)
        {
            _mapper = mapper;
        }

        #endregion

        public void Delete(int notificationId)
        {
            _mapper.Delete(notificationId);
        }

        public Notification GetNotification(int notificationId)
        {
            return _mapper.FindById(notificationId);
        }

        public IEnumerable<Notification> GetNotifications()
        {
            return _mapper.FindAll();
        }

        public void Insert(Notification data)
        {
            _mapper.Insert(data);
        }
    }
}
