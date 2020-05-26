using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Dapper.Abstract.MySQL
{
    public interface IForumPostCommentLikeDapperService
    {
        IEnumerable<ForumCommentLike> GetCommentLikes();
        void Insert(ForumCommentLike data);
        ForumCommentLike GetCommentLikeById(int likeId);
        void Delete(int likeId);
    }
}
