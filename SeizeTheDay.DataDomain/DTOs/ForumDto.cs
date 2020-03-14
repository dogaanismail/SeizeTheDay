using System;

namespace SeizeTheDay.DataDomain.DTOs
{
    public class ForumDto
    {
        public int ForumID { get; set; }
        public string ForumName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public bool? IsDefault { get; set; }
    }
}
