using System.Collections.Generic;
using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.Core.Aspects.Postsharp.CacheAspects;
using SeizeTheDay.Core.CrossCuttingConcerns.Caching.Microsoft;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Concrete.Manager.MySQL
{
    public class ForumManager : IForumService
    {
        private IForumDal _forumDal;

        public ForumManager(IForumDal forumDal)
        {
            _forumDal = forumDal;
        }
        public void Add(Forum forum)
        {
            _forumDal.Add(forum);
        }

        public void Delete(Forum forum)
        {
            _forumDal.Delete(forum);
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<Forum> GetAllLazy()
        {
            return _forumDal.StringInclude("User", "User.UserInfoe_Id", "ForumPosts","ForumPosts.User", "ForumPosts.User.UserInfoe_Id","ForumTopics");
        }

        public Forum GetByForum(int id)
        {
            return _forumDal.Find(x => x.ForumID == id);
        }

        public List<Forum> GetByForumID(int id)
        {
            return _forumDal.Query(x => x.ForumID == id);
        }

        public List<Forum> GetList()
        {
            return _forumDal.GetList();
        }

        public void Update(Forum forum)
        {
            _forumDal.Update(forum);
        }
    }
}
