using System.Collections.Generic;
using System.Linq;
using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.Core.Aspects.Postsharp.CacheAspects;
using SeizeTheDay.Core.CrossCuttingConcerns.Caching.Microsoft;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Concrete.Manager.MySQL
{
    public class ForumPostManager : IForumPostService
    {
        #region Fields
        private IForumPostDal _forumPostDal;
        #endregion

        #region Ctor

        public ForumPostManager(IForumPostDal forumPostDal)
        {
            _forumPostDal = forumPostDal;
        }

        #endregion

        public void Add(ForumPost forumpost)
        {
            _forumPostDal.Add(forumpost);
        }

        public void Delete(ForumPost forumpost)
        {
            _forumPostDal.Delete(forumpost);
        }

        //EntityCollection ForumPostComments
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<ForumPost> GetAllLazyWithoutID()
        {
            return _forumPostDal.TolistInclude( x => x.User, x=>x.User.UserInfoe_Id, x => x.Forum, x => x.ForumTopic, x => x.ForumPostComments);
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public ForumPost GetByForumPost(int id)
        {
            return _forumPostDal.Find(x => x.ForumPostID == id);
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<ForumPost> GetByForumTopicID(int id)
        {
            return _forumPostDal.Query(x => x.ForumTopicID == id);
        }

        //EntityCollection ForumPostComments && ForumPostLikes
        public ForumPost GetFirstOrDefaultInclude(int id)
        {
            return _forumPostDal.GetFirstOrDefaultInclude(x => x.ForumPostID == id,  x => x.User , x => x.ForumTopic, x => x.Forum ,  x => x.ForumPostLikes , x=> x.ForumPostComments);
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<ForumPost> GetList()
        {
            return _forumPostDal.GetList();
        }

        public ForumPost SingleInclude(int id, params string[] children)
        {
            return _forumPostDal.StringIncludeSingleWithExpression(x => x.ForumPostID == id, "User", "User.UserInfoe_Id", "ForumTopic", "Forum", "ForumPostLikes", "ForumPostComments");
        }

        public void Update(ForumPost forumpost)
        {
             _forumPostDal.Update(forumpost);
        }

    }
}
