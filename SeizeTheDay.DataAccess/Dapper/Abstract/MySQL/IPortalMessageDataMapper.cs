using SeizeTheDay.Core.DataAccess.Abstract;
using SeizeTheDay.DataDomain.DTOs;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.DataAccess.Dapper.Abstract.MySQL
{
    public interface IPortalMessageDataMapper : IDataMapper<PortalMessage>
    {
        List<PortalMessageDto> GetMessagesForApi();
    }
}
