using Dapper;
using SeizeTheDay.DataAccess.Dapper.Abstract.MySQL;
using SeizeTheDay.DataDomain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.DataAccess.Dapper.Concrete.MySQL
{
    public class PortalMessageDataMapper : AbstractDataMapper<PortalMessage>, IPortalMessageDataMapper
    {
        protected override string TableName => "PortalMessages";

        protected override string PrimaryKeyName => "MessageID";

        public PortalMessage FindById(int id)
        {
            return FindSingle($"select * from {this.TableName} WHERE {this.PrimaryKeyName}={id}", new { Id = id });
        }

        public void Insert(PortalMessage item)
        {
            using (var cn = Connection)
            {
                cn.Open();
                item.MessageID =
                    cn.Query<int>(
                        $"INSERT INTO {this.TableName} ({this.InsertQuery}) OUTPUT inserted.MessageID VALUES ({this.InsertQueryParameters})",
                        InsertParam(item)).First();
            }
        }

        public override PortalMessage Map(dynamic result)
        {
            var data = new PortalMessage
            {
                MessageID = result.MessageID,
                PortalMessageUserID = result.PortalMessageUserID,
                TextMessage = result.TextMessage,
                SendDate = result.SendDate
            };

            return data;
        }

        public void Update(PortalMessage item)
        {
            throw new NotImplementedException();
        }

        private static object InsertParam(PortalMessage item)
        {
            return new
            {
                item.MessageID,
                item.PortalMessageUserID,
                item.TextMessage,
                item.SendDate
            };
        }

        public List<PortalMessageDto> GetMessagesForApi()
        {
            using (var cn = Connection)
            {
                cn.Open();
                return cn.Query<PortalMessageDto>($"select messages.MessageID, messages.PortalMessageUserID, messages.TextMessage,messages.SendDate," +
                    $"users.Id as UserID, users.UserName, userinfo.PhotoPath, userinfo.TagUserName, userinfo.TagColor " +
                    $"from PortalMessages as messages " +
                    $"INNER JOIN Users as users ON messages.PortalMessageUserID = users.Id " +
                    $"INNER JOIN UserInfoes as userinfo ON userinfo.Id = users.Id").ToList();

            }
        }

        private string InsertQuery => "MessageID, " +
                                        "PortalMessageUserID, " +
                                        "TextMessage, " +
                                        "SendDate";

        private string InsertQueryParameters => "@MessageID, " +
                                        "@PortalMessageUserID, " +
                                        "@TextMessage, " +
                                        "@SendDate";
    }
}
