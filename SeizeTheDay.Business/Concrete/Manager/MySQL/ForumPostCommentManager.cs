using System.Collections.Generic;
using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Concrete.Manager.MySQL
{
    public class ForumPostCommentManager : IForumPostCommentService
    {

        private IForumPostCommentDal _forumPostCommentDal;

        public ForumPostCommentManager(IForumPostCommentDal forumPostComment)
        {
            _forumPostCommentDal = forumPostComment;
        }

        public void Add(ForumPostComment forumPostComment)
        {
            _forumPostCommentDal.Add(forumPostComment);
        }

        public void Delete(ForumPostComment forumPostComment)
        {
            _forumPostCommentDal.Delete(forumPostComment);
        }

        public ForumPostComment GetByForumPostComment(int id)
        {
           return  _forumPostCommentDal.Find(x => x.ForumPostCommentID == id);
        }

        public List<ForumPostComment> GetByForumPostCommentID(int id)
        {
            return _forumPostCommentDal.Query(x => x.ForumPostCommentID == id);
        }

        public List<ForumPostComment> GetByForumPostID(int id)
        {
            return _forumPostCommentDal.Query(x => x.ForumPostID == id);
        }

        //Error
        public ForumPostComment GetFirstOrDefaultInclude(int id)
        {
            return _forumPostCommentDal.GetFirstOrDefaultInclude(x => x.ForumPostCommentID == id, x=>x.ForumCommentLikes , x => x.User);
        }

        public List<ForumPostComment> GetList()
        {
            return _forumPostCommentDal.GetList();
        }

        public List<ForumPostComment> StringInclude(int id)
        {
            return _forumPostCommentDal.StringIncludeWithExpression(x => x.ForumPostID == id, "User", "User.UserInfoe_Id", "ForumCommentLikes");
        }

        public void Update(ForumPostComment forumPostComment)
        {
             _forumPostCommentDal.Update(forumPostComment);
        }
    }
}
