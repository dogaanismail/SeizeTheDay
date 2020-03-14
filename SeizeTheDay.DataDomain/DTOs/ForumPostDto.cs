using System;

namespace SeizeTheDay.DataDomain.DTOs
{
    public class ForumPostDto
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
        public string CreatedByUserName { get; set; }
        public string ForumName { get; set; }
        public string ForumTopicName { get; set; }
        public int? CommentCount { get; set; }
    }
}
