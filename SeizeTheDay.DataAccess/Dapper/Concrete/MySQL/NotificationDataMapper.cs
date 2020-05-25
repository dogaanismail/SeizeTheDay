using Dapper;
using SeizeTheDay.DataAccess.Dapper.Abstract.MySQL;
using System;
using System.Linq;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.DataAccess.Dapper.Concrete.MySQL
{
    public class NotificationDataMapper : AbstractDataMapper<Notification>, INotificationDataMapper
    {
        protected override string TableName => "Notifications";

        protected override string PrimaryKeyName => "NotificationID";

        public Notification FindById(int id)
        {
            return FindSingle($"select * from {this.TableName} WHERE {this.PrimaryKeyName}=@Id", new { Id = id });
        }

        public void Insert(Notification item)
        {
            using (var cn = Connection)
            {
                cn.Open();
                item.NotificationID =
                    cn.Query<int>(
                        $"INSERT INTO {this.TableName} ({this.InsertQuery}) OUTPUT inserted.NotificationID VALUES ({this.InsertQueryParameters})",
                        InsertParam(item)).First();
            }
        }

        public override Notification Map(dynamic result)
        {
            var data = new Notification
            {
                NotificationID = result.NotificationID,
                Type = result.Type,
                Details = result.Details,
                Title = result.Title,
                DetailsUrl = result.DetailsUrl,
                SentTo = result.SentTo,
                CreatedDate = result.CreatedDate,
                IsRead = result.IsRead
            };

            return data;
        }

        public void Update(Notification item)
        {
            throw new NotImplementedException();
        }

        private static object InsertParam(Notification item)
        {
            return new
            {
                item.NotificationID,
                item.Type,
                item.Details,
                item.Title,
                item.DetailsUrl,
                item.SentTo,
                item.CreatedDate,
                item.IsRead
            };
        }

        private string InsertQuery => "NotificationID, " +
                                        "Type, " +
                                        "Details, " +
                                        "Title, " +
                                        "DetailsUrl, " +
                                        "SentTo, " +
                                        "CreatedDate, " +
                                        "IsRead ";

        private string InsertQueryParameters => "@NotificationID, " +
                                        "@Type, " +
                                        "@Details, " +
                                        "@Title, " +
                                        "@DetailsUrl, " +
                                        "@SentTo, " +
                                        "@CreatedDate, " +
                                        "@IsRead";
    }
}
