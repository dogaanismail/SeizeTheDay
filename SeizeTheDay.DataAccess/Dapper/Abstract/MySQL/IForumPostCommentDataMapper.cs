using SeizeTheDay.Core.DataAccess.Abstract;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.DataAccess.Dapper.Abstract.MySQL
{
    public interface IForumPostCommentDataMapper : IDataMapper<ForumPostComment>
    {
        /// <summary>
        /// Returns a post comment by post id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ForumPostComment GetByPostId(int id);
    }
}
