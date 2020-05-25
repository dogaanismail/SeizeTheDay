using Dapper;
using SeizeTheDay.DataAccess.Dapper.Abstract.MySQL;
using System;
using System.Linq;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.DataAccess.Dapper.Concrete.MySQL
{
    public class ForumTopicDataMapper : AbstractDataMapper<ForumTopic>, IForumTopicDataMapper
    {
        protected override string TableName => "ForumTopic";

        protected override string PrimaryKeyName => "ForumTopicID";

        public ForumTopic FindById(int id)
        {
            return FindSingle($"select * from {this.TableName} WHERE {this.PrimaryKeyName}=@Id", new { Id = id });
        }

        public void Insert(ForumTopic item)
        {
            using (var cn = Connection)
            {
                cn.Open();
                item.ForumTopicID =
                    cn.Query<int>(
                        $"INSERT INTO {this.TableName} ({this.InsertQuery}) OUTPUT inserted.ForumTopicID VALUES ({this.InsertQueryParameters})",
                        InsertParam(item)).First();
            }
        }

        public override ForumTopic Map(dynamic result)
        {
            var data = new ForumTopic
            {
                ForumTopicID = result.ForumTopicID,
                ForumTopicName = result.ForumTopicName,
                ForumTopicDescription = result.ForumTopicDescription,
                CreatedTime = result.CreatedTime,
                CreatedBy = result.CreatedBy,
                ForumID = result.ForumID,
                ForumTopicTitle = result.ForumTopicTitle,
                IsDefault = result.IsDefault
            };

            return data;
        }

        public void Update(ForumTopic item)
        {
            throw new NotImplementedException();
        }

        private static object InsertParam(ForumTopic item)
        {
            return new
            {
                item.ForumTopicID,
                item.ForumTopicName,
                item.ForumTopicDescription,
                item.CreatedTime,
                item.CreatedBy,
                item.ForumID,
                item.ForumTopicTitle,
                item.IsDefault
            };
        }

        private string InsertQuery => "ForumTopicID, " +
                                        "ForumTopicName, " +
                                        "ForumTopicDescription, " +
                                        "CreatedTime, " +
                                        "CreatedBy, " +
                                        "ForumID, " +
                                        "ForumTopicTitle, " +
                                        "IsDefault";

        private string InsertQueryParameters => "@ForumTopicID, " +
                                        "@ForumTopicName, " +
                                        "@ForumTopicDescription, " +
                                        "@CreatedTime, " +
                                        "@CreatedBy, " +
                                        "@ForumID, " +
                                        "@ForumTopicTitle, " +
                                        "@IsDefault";
    }
}
