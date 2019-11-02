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
        private IForumPostDal _forumPostDal;

        public ForumPostManager(IForumPostDal forumPostDal)
        {
            _forumPostDal = forumPostDal;
        }
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
        public List<ForumPost> GetAllLazy(int id)
        {
            return _forumPostDal.GetAllLazyLoad(x => x.ForumPostID == id,  x => x.User, x => x.ForumTopic, x => x.Forum , x => x.ForumPostComments).ToList(); 
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
        public List<ForumPost> GetByForumPostID(int id)
        {
            return _forumPostDal.Query(x => x.ForumPostID == id);
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

        //EntityCollection ForumPostComments && ForumPostLikes
        public ForumPost GetFirstOrDefaultInclude2(int id)
        {
            return _forumPostDal.GetFirstOrDefaultInclude2(x => x.ForumPostID == id,  x => x.User, x => x.ForumTopic, x => x.Forum, x => x.ForumPostLikes, x => x.ForumPostComments);
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<ForumPost> GetList()
        {
            return _forumPostDal.GetList();
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<ForumPost> IncludeWithoutExp()
        {
            return _forumPostDal.StringInclude( "User", "User.UserInfoe_Id", "ForumTopic", "Forum", "ForumPostLikes", "ForumPostComments", "ForumPostLikes.User", "ForumPostLikes.User.UserInfoe_Id", "ForumPostComments.User.UserInfoe_Id");
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<ForumPost> MostRepliedComment(params string[] children)
        {
            return _forumPostDal.StringInclude("User", "User.UserInfoe_Id","ForumPostComments");
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<ForumPost> NewPost(params string[] children)
        {
            return _forumPostDal.StringInclude("User", "User.UserInfoe_Id", "ForumPostComments");
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public ForumPost SingleInclude(int id, params string[] children)
        {
            return _forumPostDal.StringIncludeSingleWithExpression(x => x.ForumPostID == id, "User", "User.UserInfoe_Id", "ForumTopic", "Forum", "ForumPostLikes", "ForumPostComments");

        }

         //It is very important, we are not using Lazy Loading
        [CacheAspect(typeof(MemoryCacheManager), 30)] 
        public ForumPost SingleStringIncludeWithExp(int id, params string[] children)
        {
            return _forumPostDal.StringIncludeSingleWithExpression(x => x.ForumPostID == id, "User", "User.UserInfoe_Id", "ForumTopic","Forum", "ForumPostLikes","ForumPostComments", "ForumPostLikes.User", "ForumPostLikes.User.UserInfoe_Id", "ForumPostComments.User.UserInfoe_Id");
        }

        public List<ForumPost> StringIncludeWithExp(int id, params string[] children)
        {
            throw new System.NotImplementedException();
        }

        public void Update(ForumPost forumpost)
        {
             _forumPostDal.Update(forumpost);
        }

    }
}
