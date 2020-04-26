namespace SeizeTheDay.Core.Constants
{
    public class ApiUrlConstants
    {
        #region Base
        public const string BaseUrl = "https://localhost:44387/api/";
        #endregion

        #region PortalMessages
        public const string GetPortalMessages = "https://localhost:44387/api/messages/getmessages";
        public const string CreatePortalMessage = "https://localhost:44387/api/messages/createmessage";
        public const string DeletePortalMessage = "https://localhost:44387/api/messages/deletemessage";
        #endregion

        #region Forum
        public const string GetForums = "https://localhost:44387/api/forums/getforums";
        public const string GetForumById = "https://localhost:44387/api/forums/getbyid";
        public const string CreateForum = "https://localhost:44387/api/forums/createforum";
        public const string DeleteForum = "https://localhost:44387/api/forums/deleteforum";
        public const string UpdateForum = "https://localhost:44387/api/forums/updateforum";
        #endregion

        #region ForumPost
        public const string GetForumPosts = "https://localhost:44387/api/forumposts/getposts";
        public const string GetForumPostById = "https://localhost:44387/api/forumposts/getpostbyid";
        public const string CreateForumPost = "https://localhost:44387/api/forumposts/createpost";
        public const string DeleteForumPost = "https://localhost:44387/api/forumposts/deletepost";
        public const string UpdateForumPost = "https://localhost:44387/api/forumposts/updatepost";
        #endregion

        #region ForumPostDetail
        public const string GetForumPostDetailById = "https://localhost:44387/api/postdetail/getdetailsbyid";
        public const string DeleteForumPostDetail = "https://localhost:44387/api/postdetail/editpostdetail";
        public const string UpdateForumPostDetail = "https://localhost:44387/api/postdetail/deletepost";
        #endregion

        #region ForumTopic
        public const string GetForumTopics = "https://localhost:44387/api/forumtopics/gettopics";
        public const string GetForumTopicById = "https://localhost:44387/api/forumtopics/getbyid";
        public const string GetForumTopicByForumId = "https://localhost:44387/api/forumtopics/getbyforumid";
        public const string CreateForumTopic = "https://localhost:44387/api/forumtopics/createtopic";
        public const string DeleteForumTopic = "https://localhost:44387/api/forumtopics/deletetopic";
        public const string UpdateForumTopic = "https://localhost:44387/api/forumtopics/updatetopic";
        #endregion
    }
}
