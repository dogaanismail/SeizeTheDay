using Dapper;
using SeizeTheDay.DataAccess.Dapper.Abstract.MySQL;
using System;
using System.Linq;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.DataAccess.Dapper.Concrete.MySQL
{
    public class ChatDataMapper : AbstractDataMapper<Chat>, IChatDataMapper
    {
        protected override string TableName => "Chat";

        protected override string PrimaryKeyName => "ChatID";

        public Chat FindById(int id)
        {
            return FindSingle($"select * from {this.TableName} WHERE {this.PrimaryKeyName}={id}", new { Id = id });
        }

        public void Insert(Chat item)
        {
            using (var cn = Connection)
            {
                cn.Open();
                item.ChatID =
                    cn.Query<int>(
                        $"INSERT INTO {this.TableName} ({this.InsertQuery}) OUTPUT inserted.ChatID VALUES ({this.InsertQueryParameters})",
                        InsertParam(item)).First();
            }
        }

        public override Chat Map(dynamic result)
        {
            var data = new Chat
            {
                ChatID = result.ChatID,
                Text = result.Text,
                ChatBoxID = result.ChatBoxID,
                SenderID = result.SenderID,
                ReceiverID = result.ReceiverID,
                SentDate = result.SentDate,
                IsRead = result.IsRead
            };

            return data;
        }

        public void Update(Chat item)
        {
            throw new NotImplementedException();
        }

        private static object InsertParam(Chat item)
        {
            return new
            {
                item.ChatID,
                item.Text,
                item.ChatBoxID,
                item.SenderID,
                item.ReceiverID,
                item.SentDate,
                item.IsRead
            };
        }

        private string InsertQuery => "ChatID, " +
                                        "Text, " +
                                        "ChatBoxID, " +
                                        "SenderID, " +
                                        "ReceiverID, " +
                                        "SentDate, " +
                                        "IsRead ";

        private string InsertQueryParameters => "@ChatID, " +
                                        "@Text, " +
                                        "@ChatBoxID, " +
                                        "@SenderID, " +
                                        "@ReceiverID, " +
                                        "@SentDate, " +
                                        "@IsRead";
    }
}
