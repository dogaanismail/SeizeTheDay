using System;

namespace SeizeTheDay.DataDomain.Api
{
    public class ForumPostApi
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
    }
}
