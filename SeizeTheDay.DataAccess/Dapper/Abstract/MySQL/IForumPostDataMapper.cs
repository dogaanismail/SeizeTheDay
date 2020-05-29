using SeizeTheDay.Core.DataAccess.Abstract;
using SeizeTheDay.DataDomain.DTOs;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.DataAccess.Dapper.Abstract.MySQL
{
    public interface IForumPostDataMapper : IDataMapper<ForumPost>
    {
        /// <summary>
        /// Returns a forum posts list without include.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ForumPost> GetForumPosts();

        /// <summary>
        /// Returns post detail by post id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TopicDetailDto GetPostDetailById(int id);

        /// <summary>
        /// Returns a forum posts list with include.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TopicDetailDto> GetPosts();
    }
}
