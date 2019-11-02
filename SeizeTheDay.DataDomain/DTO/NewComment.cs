namespace SeizeTheDay.Entities.EntityClasses.MySQL
{
    public class NewComment
    {
        public int? CommentID { get; set; }
        public int? PostID { get; set; }
        public string Text { get; set; }
    }
}
