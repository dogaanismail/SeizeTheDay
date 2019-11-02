namespace SeizeTheDay.Entities.EntityClasses.MySQL
{
    public class PostComments
    {
        public int? CommentID { get; set; }
        public string Text { get; set; }
        public string CreatedTime { get; set; }
        public string CreatedBy { get; set; }
        public int? ForumPostID { get; set; }

        //CreatedByUserName
        public string CreatedByUserName { get; set; }

        //CreatedByID
        public string CreatedByUserID { get; set; }

        //CreatedByID
        public string CreatedByPhotoPath { get; set; }

        //ForumPostLikes
        public string CommentLikesCount { get; set; }
    }
}
