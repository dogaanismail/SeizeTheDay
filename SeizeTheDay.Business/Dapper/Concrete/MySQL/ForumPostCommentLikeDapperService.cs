using SeizeTheDay.Business.Dapper.Abstract.MySQL;
using SeizeTheDay.DataAccess.Dapper.Abstract.MySQL;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Dapper.Concrete.MySQL
{
    public class ForumPostCommentLikeDapperService : IForumPostCommentLikeDapperService
    {
        #region Ctor
        private readonly IForumPostCommentLikeDataMapper _mapper;

        public ForumPostCommentLikeDapperService(IForumPostCommentLikeDataMapper mapper)
        {
            _mapper = mapper;
        }

        #endregion

        public void Delete(int likeId)
        {
            _mapper.Delete(likeId);
        }

        public ForumCommentLike GetCommentLikeById(int likeId)
        {
            return _mapper.FindById(likeId);
        }

        public IEnumerable<ForumCommentLike> GetCommentLikes()
        {
            return _mapper.FindAll();
        }

        public void Insert(ForumCommentLike data)
        {
            _mapper.Insert(data);
        }
    }
}
