using System.Collections.Generic;
using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.Core.Aspects.Postsharp.CacheAspects;
using SeizeTheDay.Core.CrossCuttingConcerns.Caching.Microsoft;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Concrete.Manager.MySQL
{
    public class ForumTopicManager : IForumTopicService
    {
        #region Fields

        private IForumTopicDal _forumTopicDal;
        #endregion

        #region Ctor
        public ForumTopicManager(IForumTopicDal forumTopicDal)
        {
            _forumTopicDal = forumTopicDal;
        }
        #endregion

        public void Add(ForumTopic forumtopic)
        {
            _forumTopicDal.Add(forumtopic);
        }

        public void Delete(ForumTopic forumtopic)
        {
            _forumTopicDal.Delete(forumtopic);
        }

        //Error ForumPosts EntityCollection
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public ForumTopic FirstOrDefaultInclude(int id)
        {
            return _forumTopicDal.GetFirstOrDefaultInclude(x => x.ForumTopicID == id, x => x.Forum, x => x.User, x => x.ForumPosts);

        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public ForumTopic GetByForumTopic(int id)
        {
            return _forumTopicDal.Find(x => x.ForumTopicID == id);
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<ForumTopic> GetByForumTopicID(int id)
        {
            return _forumTopicDal.Query(x => x.ForumTopicID == id);
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<ForumTopic> GetList()
        {
            return _forumTopicDal.GetList();
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public ForumTopic SingleStringIncludeWithExp(int id, params string[] children)
        {
            return _forumTopicDal.StringIncludeSingleWithExpression(x => x.ForumTopicID == id, "Forum", "User", "User.UserInfoe_Id", "ForumPosts", "ForumPosts.User", "ForumPosts.User.UserInfoe_Id");
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<ForumTopic> StringIncludeWithExpression(int id,params string[] children)
        {
            return _forumTopicDal.StringIncludeWithExpression(x => x.ForumTopicID == id, "Forum", "User", "User.UserInfoe_Id", "ForumPosts", "ForumPosts.User", "ForumPosts.User.UserInfoe_Id");
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<ForumTopic> StringIncludeWithoutExpression(params string[] children)
        {
            return _forumTopicDal.StringInclude("Forum", "User", "User.UserInfoe_Id", "ForumPosts", "ForumPosts.User", "ForumPosts.User.UserInfoe_Id");
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<ForumTopic> TolistIncludeByForumID(int id)
        {
            return _forumTopicDal.GetAllLazyLoad(x => x.ForumID == id, x =>x.Forum);
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<ForumTopic> TolistIncludeWithoutID()
        {
            return _forumTopicDal.TolistInclude(x => x.Forum);
        }

        public void Update(ForumTopic forumtopic)
        {
            _forumTopicDal.Update(forumtopic);
        }
    }
}
