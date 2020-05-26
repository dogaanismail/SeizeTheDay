using SeizeTheDay.Core.DataAccess.Abstract;
using SeizeTheDay.DataDomain.DTOs;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.DataAccess.Dapper.Abstract.MySQL
{
    public interface IForumPostDataMapper : IDataMapper<ForumPost>
    {
        IEnumerable<ForumPost> GetForumPosts();

        /// <summary>
        /// Returns post detail by post id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TopicDetailDto GetPostDetailById(int id);
    }
}
