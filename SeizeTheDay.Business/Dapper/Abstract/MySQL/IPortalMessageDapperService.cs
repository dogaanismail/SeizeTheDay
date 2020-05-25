using SeizeTheDay.DataDomain.DTOs;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Dapper.Abstract.MySQL
{
    public interface IPortalMessageDapperService
    {
        List<PortalMessageDto> GetMessages();
        void Insert(PortalMessage data);
        void Delete(int messageId);
    }
}
