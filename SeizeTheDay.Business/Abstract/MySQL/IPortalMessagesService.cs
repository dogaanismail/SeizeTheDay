using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Abstract.MySQL
{
    public interface IPortalMessagesService
    {
        List<PortalMessage> GetList();
        void Add(PortalMessage messages);
        void Delete(PortalMessage messages);
        void Update(PortalMessage messages);
        PortalMessage GetByMessageID(int id);
        List<PortalMessage> GetByMessageIDToList(int id);
        PortalMessage GetByUserID(string id);
        List<PortalMessage> GetByUserIDToList(string id);
        PortalMessage FirstOrDefault();
        List<PortalMessage> GetAllLazyWithoutID();
    }
}
