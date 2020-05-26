using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Dapper.Abstract.MySQL
{
    public interface IForumPostLikeDapperService
    {
        IEnumerable<ForumPostLike> GetPostLikes();
        void Insert(ForumPostLike data);
        ForumPostLike GetPostLikeById(int likeId);
        void Delete(int likeId);
    }
}
