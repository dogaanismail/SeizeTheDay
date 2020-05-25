using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Dapper.Abstract.MySQL
{
    public interface INotificationDapperService
    {
        IEnumerable<Notification> GetNotifications();
        void Insert(Notification data);
        Notification GetNotification(int notificationId);
        void Delete(int notificationId);
    }
}
