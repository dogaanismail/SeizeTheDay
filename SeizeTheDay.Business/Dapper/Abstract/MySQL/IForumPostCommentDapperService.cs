using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Dapper.Abstract.MySQL
{
    public interface IForumPostCommentDapperService
    {
        /// <summary>
        /// Returns all comment.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ForumPostComment> GetComments();

        void Insert(ForumPostComment data);

        /// <summary>
        /// Returns a comment by id.
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        ForumPostComment GetCommentById(int commentId);
        /// <summary>
        /// Returns a comment by post id.
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        ForumPostComment GetByPostId(int postId);

        void Delete(int commentId);
    }
}
