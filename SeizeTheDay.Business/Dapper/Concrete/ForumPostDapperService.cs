using SeizeTheDay.Business.Dapper.Abstract;
using SeizeTheDay.DataAccess.Dapper.Abstract;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Dapper.Concrete
{
    public class ForumPostDapperService : IForumPostDapperService
    {
        private readonly IForumPostDataMapper _mapper;

        public ForumPostDapperService(IForumPostDataMapper mapper)
        {
            _mapper = mapper;
        }
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
    }
}
