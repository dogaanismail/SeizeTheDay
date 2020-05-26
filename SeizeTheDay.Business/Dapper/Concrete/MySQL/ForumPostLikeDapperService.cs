using SeizeTheDay.Business.Dapper.Abstract.MySQL;
using SeizeTheDay.DataAccess.Dapper.Abstract.MySQL;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Dapper.Concrete.MySQL
{
    public class ForumPostLikeDapperService : IForumPostLikeDapperService
    {
        #region Ctor
        private readonly IForumPostLikeDataMapper _mapper;

        public ForumPostLikeDapperService(IForumPostLikeDataMapper mapper)
        {
            _mapper = mapper;
        }

        #endregion

        public void Delete(int likeId)
        {
            _mapper.Delete(likeId);
        }

        public ForumPostLike GetPostLikeById(int likeId)
        {
            return _mapper.FindById(likeId);
        }

        public IEnumerable<ForumPostLike> GetPostLikes()
        {
            return _mapper.FindAll();
        }

        public void Insert(ForumPostLike data)
        {
            _mapper.Insert(data);
        }
    }
}
