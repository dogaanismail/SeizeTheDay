using System;

namespace SeizeTheDay.Entities.EntityClasses.MySQL
{
    public class ForumPost
    {
        public int ForumPostID { get; set; }
        public string ForumPostTitle { get; set; }
        public string ForumPostContent { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public int? ForumTopicID { get; set; }
        public int? ForumID { get; set; }
        public bool? ShowInPortal { get; set; }
        public bool? PostLocked { get; set; }
        public int? ReviewCount { get; set; }
        public bool? IsDefault { get; set; }

        //CreatedByUserName
        public string CreatedByUserName { get; set; }

        //ForumName
        public string ForumName { get; set; }

        //ForumTopic
        public string ForumTopicName { get; set; }

        public int? CommentCount { get; set; }
    }
}
