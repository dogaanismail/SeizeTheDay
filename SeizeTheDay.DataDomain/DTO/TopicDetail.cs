namespace SeizeTheDay.Entities.EntityClasses.MySQL
{
    public class TopicDetail
    {
        public int ForumPostID { get; set; }
        public string ForumPostTitle { get; set; }
        public string ForumPostContent { get; set; }
        public string CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public int? ForumTopicID { get; set; }
        public int? ForumID { get; set; }
        public bool? ShowInPortal { get; set; }
        public bool? PostLocked { get; set; }
        public int? ReviewCount { get; set; }
        public bool? IsDefault { get; set; }

        //CreatedByUserName
        public string CreatedByUserName { get; set; }

        //CreatedByID
        public string CreatedByUserID { get; set; }

        //CreatedByID
        public string CreatedByPhotoPath { get; set; }

        //ForumName
        public string ForumName { get; set; }

        //ForumTopic
        public string ForumTopicName { get; set; }

        //ForumComment
        public string CommentCount { get; set; }

        //ForumPostLikes
        public string PostLikesCount { get; set; }




    }
}
