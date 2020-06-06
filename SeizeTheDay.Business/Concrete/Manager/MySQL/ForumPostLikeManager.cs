using System.Collections.Generic;
using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Concrete.Manager.MySQL
{
    public class ForumPostLikeManager : IForumPostLikeService
    {
        #region Ctor
        private IForumPostLikeDal _forumPostLikeDal;

        public ForumPostLikeManager(IForumPostLikeDal forumPostLikeDal)
        {
            _forumPostLikeDal = forumPostLikeDal;
        }
        #endregion

        public void Add(ForumPostLike forumpostLike)
        {
            _forumPostLikeDal.Add(forumpostLike);
        }

        public void Delete(ForumPostLike forumPostLike)
        {
            _forumPostLikeDal.Delete(forumPostLike);
        }

        public ForumPostLike GetByForumPostLike(int id)
        {
            return _forumPostLikeDal.Find(x => x.LikeID == id);
        }

        public List<ForumPostLike> GetByForumPostLikeID(int id)
        {
            return _forumPostLikeDal.Query(x => x.LikeID == id);
        }

        public ForumPostLike GetByUserandPostID(string userID, int postID)
        {
            return _forumPostLikeDal.Find(x => x.UserID == userID && x.PostID == postID);
        }

        public List<ForumPostLike> GetList()
        {
            return _forumPostLikeDal.GetList();
        }

        public void Update(ForumPostLike forumPostLike)
        {
            _forumPostLikeDal.Update(forumPostLike);
        }
    }
}
