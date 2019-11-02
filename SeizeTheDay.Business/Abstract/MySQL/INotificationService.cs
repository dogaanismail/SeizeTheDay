using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Abstract.MySQL
{
    public interface INotificationService
    {       
        //Get by NotificationID
        List<Notification> GetByNotfIDList(int id);
        Notification GetByNotificationID(int id);

        //Get by  UserID
        List<Notification> GetByUserIDList(string id);
        Notification GetByUserID(string id);

        void Update(Notification notification);
        void Add(Notification notification);
        List<Notification> GetList();
        void Delete(Notification notification);
    }
}
