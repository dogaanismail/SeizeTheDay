using Dapper;
using SeizeTheDay.DataAccess.Dapper.Abstract.MySQL;
using System;
using System.Linq;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.DataAccess.Dapper.Concrete.MySQL
{
    public class ChatBoxDataMapper : AbstractDataMapper<ChatBox>, IChatBoxDataMapper
    {
        protected override string TableName => "ChatBox";

        protected override string PrimaryKeyName => "ChatboxID";

        public ChatBox FindById(int id)
        {
            return FindSingle($"select * from {this.TableName} WHERE {this.PrimaryKeyName}=@Id", new { Id = id });
        }

        public void Insert(ChatBox item)
        {
            using (var cn = Connection)
            {
                cn.Open();
                item.ChatboxID =
                    cn.Query<int>(
                        $"INSERT INTO {this.TableName} ({this.InsertQuery}) OUTPUT inserted.ChatboxID VALUES ({this.InsertQueryParameters})",
                        InsertParam(item)).First();
            }
        }

        public override ChatBox Map(dynamic result)
        {
            var data = new ChatBox
            {
                ChatboxID = result.ChatboxID,
                Title = result.Title,
                SenderID = result.SenderID,
                CreatedDate = result.CreatedDate,
                ReceiverID = result.ReceiverID
            };

            return data;
        }

        public void Update(ChatBox item)
        {
            throw new NotImplementedException();
        }

        private static object InsertParam(ChatBox item)
        {
            return new
            {
                item.ChatboxID,
                item.Title,
                item.SenderID,
                item.CreatedDate,
                item.ReceiverID
            };
        }

        private string InsertQuery => "ChatboxID, " +
                                        "Title, " +
                                        "SenderID, " +
                                        "CreatedDate, " +
                                        "ReceiverID";

        private string InsertQueryParameters => "@ChatboxID, " +
                                        "@Title," +
                                        "@SenderID," +
                                        "@CreatedDate," +
                                        "@ReceiverID";
    }
}
