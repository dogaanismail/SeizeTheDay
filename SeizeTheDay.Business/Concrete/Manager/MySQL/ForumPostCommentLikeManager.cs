using System.Collections.Generic;
using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Concrete.Manager.MySQL
{
    public class ForumPostCommentLikeManager : IForumPostCommentLikeService
    {

        private IForumCommentLikeDal _forumCommentLikeDal;

        public ForumPostCommentLikeManager(IForumCommentLikeDal forumCommentLikeDal)
        {
            _forumCommentLikeDal = forumCommentLikeDal;
        }
        public void Add(ForumCommentLike forumCommentLike)
        {
            _forumCommentLikeDal.Add(forumCommentLike);
        }

        public void Delete(ForumCommentLike forumCommentLike)
        {
            _forumCommentLikeDal.Delete(forumCommentLike);
        }

        public ForumCommentLike GetByComment(int id)
        {
            return _forumCommentLikeDal.Find(x => x.CommentID == id);
        }

        public ForumCommentLike GetByCommentandUserID(string userID, int commentID)
        {
            return _forumCommentLikeDal.Find(x => x.UserID == userID && x.CommentID == commentID);
        }

        public List<ForumCommentLike> GetByForumCommentIDTolist(int id)
        {
            return _forumCommentLikeDal.Query(x => x.CommentID == id);
        }

        public ForumCommentLike GetByForumCommentLike(int id)
        {
           return _forumCommentLikeDal.Find(x => x.CommentLikeID == id);
        }

        public List<ForumCommentLike> GetByForumCommentLikeID(int id)
        {
            return _forumCommentLikeDal.Query(x => x.CommentLikeID == id);
        }

        public List<ForumCommentLike> GetList()
        {
            return _forumCommentLikeDal.GetList();
        }

        public void Update(ForumCommentLike forumCommentLike)
        {
            _forumCommentLikeDal.Update(forumCommentLike);
        }
    }
}
