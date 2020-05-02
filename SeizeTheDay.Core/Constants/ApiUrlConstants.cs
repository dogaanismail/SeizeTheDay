namespace SeizeTheDay.Core.Constants
{
    public class ApiUrlConstants
    {
        #region Base
        public const string BaseUrl = "https://localhost:44367/api/";
        #endregion

        #region PortalMessage
        public const string GetPortalMessages = "https://localhost:44367/api/messages/getmessages";
        public const string CreatePortalMessage = "https://localhost:44367/api/messages/createmessage";
        public const string DeletePortalMessage = "https://localhost:44367/api/messages/deletemessage";
        #endregion

        #region Forum
        public const string GetForums = "https://localhost:44367/api/forums/getforums";
        public const string GetForumById = "https://localhost:44367/api/forums/getbyid";
        public const string CreateForum = "https://localhost:44367/api/forums/createforum";
        public const string DeleteForum = "https://localhost:44367/api/forums/deleteforum";
        public const string UpdateForum = "https://localhost:44367/api/forums/updateforum";
        #endregion

        #region ForumPost
        public const string GetForumPosts = "https://localhost:44367/api/forumposts/getposts";
        public const string GetForumPostById = "https://localhost:44367/api/forumposts/getpostbyid";
        public const string CreateForumPost = "https://localhost:44367/api/forumposts/createpost";
        public const string DeleteForumPost = "https://localhost:44367/api/forumposts/deletepost";
        public const string UpdateForumPost = "https://localhost:44367/api/forumposts/updatepost";
        #endregion

        #region ForumPostDetail
        public const string GetForumPostDetailById = "https://localhost:44367/api/postdetail/getdetailsbyid";
        public const string DeleteForumPostDetail = "https://localhost:44367/api/postdetail/deletepost";
        public const string UpdateForumPostDetail = "https://localhost:44367/api/postdetail/editpostdetail";
        #endregion

        #region ForumPostComment
        public const string GetComments = "https://localhost:44367/api/comments/getcomments";
        public const string GetCommentsByPostId = "https://localhost:44367/api/comments/getlistbypostid";   
        public const string CreateComment = "https://localhost:44367/api/comments/createcomment";
        public const string DeleteComment = "https://localhost:44367/api/comments/deletecomment";
        public const string UpdateComment = "https://localhost:44367/api/comments/updatecomment";
        #endregion

        #region ForumTopic
        public const string GetForumTopics = "https://localhost:44367/api/forumtopics/gettopics";
        public const string GetForumTopicById = "https://localhost:44367/api/forumtopics/getbyid";
        public const string GetForumTopicByForumId = "https://localhost:44367/api/forumtopics/getbyforumid";
        public const string CreateForumTopic = "https://localhost:44367/api/forumtopics/createtopic";
        public const string DeleteForumTopic = "https://localhost:44367/api/forumtopics/deletetopic";
        public const string UpdateForumTopic = "https://localhost:44367/api/forumtopics/updatetopic";
        #endregion

        #region User
        public const string GetUsers = "https://localhost:44367/api/users/getusers";
        public const string GetNameList = "https://localhost:44367/api/users/getnamelist";
        public const string GetUserById = "https://localhost:44367/api/users/getbyid";
        public const string CreateUser = "https://localhost:44367/api/users/createuser";
        public const string DeleteUser = "https://localhost:44367/api/users/deleteuser";
        public const string UpdateUser = "https://localhost:44367/api/users/updateuser";
        #endregion

        #region Role
        public const string GetRoles = "https://localhost:44367/api/roles/getroles";
        public const string GetRoleById = "https://localhost:44367/api/roles/getbyid";
        public const string CreateRole = "https://localhost:44367/api/roles/createrole";
        public const string DeleteRole = "https://localhost:44367/api/roles/deleterole";
        public const string UpdateRole = "https://localhost:44367/api/roles/updaterole";
        #endregion

        #region Module
        public const string GetModules = "https://localhost:44367/api/modules/getmodules";
        public const string GetModuleById = "https://localhost:44367/api/modules/getbyid";
        public const string CreateModule = "https://localhost:44367/api/modules/createmodule";
        public const string DeleteModule = "https://localhost:44367/api/modules/deletemodule";
        public const string UpdateModule = "https://localhost:44367/api/modules/updatemodule";
        #endregion

        #region Chat
        public const string GetChatByBoxId = "https://localhost:44367/api/chat/getchatsbyboxid";
        public const string DeleteChat = "https://localhost:44367/api/chat/deletechats";
        #endregion

        #region Chatbox
        public const string GetChatBoxesByUserId = "https://localhost:44367/api/chatbox/getchatboxesbyuserid";
        public const string CreateChatBox = "https://localhost:44367/api/chatbox/createchatbox";
        public const string DeleteChatBox = "https://localhost:44367/api/chatbox/deletechatbox";
        #endregion

        #region Notification
        public const string GetNotifications = "https://localhost:44367/api/notifications/getnotifications";
        public const string GetMessageNotifications = "https://localhost:44367/api/notifications/getmessagenot";
        public const string CreateNotification = "https://localhost:44367/api/notifications/createnotification";
        public const string UpdateNotification = "https://localhost:44367/api/notifications/updatenotification";
        public const string DeleteNotification = "https://localhost:44367/api/notifications/deletenotification";
        #endregion
    }
}
