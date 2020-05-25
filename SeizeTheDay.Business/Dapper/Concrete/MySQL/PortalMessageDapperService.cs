using SeizeTheDay.Business.Dapper.Abstract.MySQL;
using SeizeTheDay.DataAccess.Dapper.Abstract.MySQL;
using SeizeTheDay.DataDomain.DTOs;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Dapper.Concrete.MySQL
{
    public class PortalMessageDapperService : IPortalMessageDapperService
    {
        #region Ctor
        private readonly IPortalMessageDataMapper _mapper;

        public PortalMessageDapperService(IPortalMessageDataMapper mapper)
        {
            _mapper = mapper;
        }

        #endregion

        public void Delete(int messageId)
        {
            _mapper.Delete(messageId);
        }

        public List<PortalMessageDto> GetMessages()
        {
            return _mapper.GetMessagesForApi();
        }

        public void Insert(PortalMessage data)
        {
            _mapper.Insert(data);
        }
    }
}
