using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeizeTheDay.DataDomain.DTOs
{
    public class PostWidgetDto
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
        public string CreatedByUserName { get; set; }
        public string CreatedByUserID { get; set; }
        public string CreatedByPhotoPath { get; set; }
        public string ForumName { get; set; }
        public string ForumTopicName { get; set; }
        public string CommentCount { get; set; }
        public string PostLikesCount { get; set; }
    }
}
