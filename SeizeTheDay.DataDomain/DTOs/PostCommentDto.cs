namespace SeizeTheDay.DataDomain.DTOs
{
    public class PostCommentDto
    {
        public int? CommentID { get; set; }
        public string Text { get; set; }
        public string CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public int? ForumPostID { get; set; }
        public string CreatedByUserName { get; set; }
        public string CreatedByUserID { get; set; }
        public string CreatedByPhotoPath { get; set; }
        public string CommentLikesCount { get; set; }
    }
}
