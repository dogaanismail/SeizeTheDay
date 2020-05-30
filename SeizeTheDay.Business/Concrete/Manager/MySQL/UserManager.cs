using System;
using System.Collections.Generic;
using SeizeTheDay.Business.Abstract.MySQL;
using SeizeTheDay.Core.Aspects.Postsharp.CacheAspects;
using SeizeTheDay.Core.CrossCuttingConcerns.Caching.Microsoft;
using SeizeTheDay.DataAccess.Abstract.MySQL;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Business.Concrete.Manager.MySQL
{

    public class UserManager : IUserService
    {
        private IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void AddUser(User user)
        {
            _userDal.Add(user);
        }

        public void Delete(User user)
        {
            _userDal.Delete(user);
        }

        public User GetByEmail(string email)
        {
            return _userDal.Find(x => x.Email == email);
        }

        public User GetByUserID(string userID)
        {
            return _userDal.Find(x => x.Id == userID);
        }

        public User GetByUserName(string UserName)
        {
            return _userDal.Find(x => x.UserName == UserName);
        }

        //It is important 
        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public User GetFirstOrDefaultInclude(string id)
        {
            return _userDal.GetFirstOrDefaultInclude(x => x.Id == id, x => x.UserInfoe_Id.Country, x => x.ForumPosts, x => x.ForumPostComments, x => x.FriendRequests_FutureFriendID, x => x.FriendRequests_UserID, x => x.Friends_UserID, x => x.Friends_FutureFriendID, x => x.ForumPostLikes, x => x.ForumCommentLikes, x => x.ChatBoxes_ReceiverID, x => x.ChatBoxes_SenderID, x => x.Chats_SenderID, x => x.Chats_ReceiverID, x => x.UserInfoe_Id, x => x.ProfileVisitors_VisitorID, x => x.ProfileVisitors_UserID, x => x.UserInfoe_Id.UserType);

        }

        public List<User> GetList()
        {
            return _userDal.GetList();
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public User GetUserNotifications(string userName, params string[] children)
        {
            return _userDal.StringIncludeSingleWithExpression(x => x.UserName == userName, "Notifications");
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public User SingleStringIncludeWithExp(string id, params string[] children)
        {
            return _userDal.StringIncludeSingleWithExpression(x => x.Id == id, "UserInfoe_Id", "ForumPosts", "ForumPostComments", "ForumPostComments.ForumPost", "ForumPostComments.ForumPost.ForumTopic", "ForumPostComments.ForumPost.ForumPostComments", "ForumPostComments.ForumPost.ForumPostLikes", "ForumPostComments.User", "ForumPostComments.User.UserInfoe_Id", "ForumPostComments.User.UserInfoe_Id", "UserInfoe_Id.Country", "FriendRequests_FutureFriendID", "FriendRequests_UserID", "Friends_UserID", "Friends_FutureFriendID", "ProfileVisitors_VisitorID", "ProfileVisitors_VisitorID.User_UserID", "ProfileVisitors_VisitorID.User_UserID.UserInfoe_Id", "ProfileVisitors_UserID", "ProfileVisitors_UserID.User_UserID", "ProfileVisitors_UserID.User_UserID.UserInfoe_Id", "UserInfoe_Id.UserType");
            //return _userDal.StringIncludeSingleWithExpression(x => x.Id == id, "UserInfoe_Id", "x.UserInfoe_Id.Country", "ForumPosts", "ForumPostComments", "FriendRequests_FutureFriendID", "FriendRequests_UserID", "Friends_UserID", "Friends_FutureFriendID", "ForumPostLikes", "ForumCommentLikes", "ChatBoxes_ReceiverID", "ChatBoxes_SenderID", "Chats_SenderID", "Chats_ReceiverID", "ProfileVisitors_VisitorID", "ProfileVisitors_VisitorID.User_UserID", "ProfileVisitors_VisitorID.User_UserID.UserInfoe_Id", "ProfileVisitors_UserID", "ProfileVisitors_UserID.User_UserID", "ProfileVisitors_UserID.User_UserID.UserInfoe_Id", "UserInfoe_Id.UserType");
        }

        [CacheAspect(typeof(MemoryCacheManager), 30)]
        public List<User> StringIncludeWithoutExpression()
        {
            return _userDal.StringInclude("UserInfoe_Id");
        }

        public List<User> TolistInclude()
        {
            return _userDal.TolistInclude(x => x.UserInfoe_Id);

        }

        public void Update(User User)
        {
            _userDal.Update(User);
        }
    }
}
