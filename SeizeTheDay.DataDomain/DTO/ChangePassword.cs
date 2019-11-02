namespace SeizeTheDay.Entities.EntityClasses.MySQL
{
    public class ChangePassword
    {
        public string OldPass { get; set; }
        public string NewPass { get; set; }
        public string CheckPass { get; set; }
    }
}
