using System;

namespace SeizeTheDay.DataDomain.DTOs
{
    public class ForumTopicDto
    {
        public int ForumTopicID { get; set; }
        public string ForumTopicName { get; set; }
        public string ForumTopicDescription { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public int? ForumID { get; set; }
        public string ForumTopicTitle { get; set; }
        public string ForumName { get; set; }
        public bool? IsDefault { get; set; }
    }
}
