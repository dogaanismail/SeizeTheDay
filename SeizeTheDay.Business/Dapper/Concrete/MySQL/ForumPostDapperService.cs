using SeizeTheDay.Business.Dapper.Abstract.MySQL;
using SeizeTheDay.DataAccess.Dapper.Abstract.MySQL;
using SeizeTheDay.DataDomain.DTOs;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Dapper.Concrete.MySQL
{
    public class ForumPostDapperService : IForumPostDapperService
    {
        #region Ctor
        private readonly IForumPostDataMapper _mapper;

        public ForumPostDapperService(IForumPostDataMapper mapper)
        {
            _mapper = mapper;
        }

        #endregion

        public IEnumerable<ForumPost> GetForumPosts()
        {
            return _mapper.FindAll();
        }

        public ForumPost GetForumPost(int forumPostId)
        {
            return _mapper.FindById(forumPostId);
        }

        public void Insert(ForumPost data)
        {
            _mapper.Insert(data);
        }

        public void Delete(int forumPostId)
        {
            _mapper.Delete(forumPostId);
        }

        public TopicDetailDto GetPostDetailById(int postId)
        {
            return _mapper.GetPostDetailById(postId);
        }
    }
}
