namespace SeizeTheDay.DataDomain.DTOs
{
    public class NotificationDto
    {
        public int NotificationID { get; set; }
        public int? Type { get; set; }
        public string Details { get; set; }
        public string Title { get; set; }
        public string DetailsUrl { get; set; }
        public string SentTo { get; set; }
        public string CreatedDate { get; set; }
        public bool? IsRead { get; set; }
        public int TotalNotification { get; set; }
    }
}
