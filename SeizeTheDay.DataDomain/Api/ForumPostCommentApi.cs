namespace SeizeTheDay.DataDomain.Api
{
    public class ForumPostCommentApi
    {
        public int CommentID { get; set; }
        public int? PostID { get; set; }
        public string Text { get; set; }
    }
}
