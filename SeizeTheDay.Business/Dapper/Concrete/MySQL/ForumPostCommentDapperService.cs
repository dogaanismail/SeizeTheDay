using SeizeTheDay.Business.Dapper.Abstract.MySQL;
using SeizeTheDay.DataAccess.Dapper.Abstract.MySQL;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Dapper.Concrete.MySQL
{
    public class ForumPostCommentDapperService : IForumPostCommentDapperService
    {
        #region Ctor
        private readonly IForumPostCommentDataMapper _mapper;

        public ForumPostCommentDapperService(IForumPostCommentDataMapper mapper)
        {
            _mapper = mapper;
        }

        #endregion

        public void Delete(int commentId) => _mapper.Delete(commentId);

        public ForumPostComment GetByPostId(int postId) => _mapper.GetByPostId(postId);

        public IEnumerable<ForumPostComment> GetComment() => _mapper.FindAll();

        public ForumPostComment GetCommentById(int commentId) => _mapper.FindById(commentId);

        public void Insert(ForumPostComment data) => _mapper.Insert(data);
    }
}
