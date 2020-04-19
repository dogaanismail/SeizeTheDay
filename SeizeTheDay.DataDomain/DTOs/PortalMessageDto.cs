namespace SeizeTheDay.DataDomain.DTOs
{
    public class PortalMessageDto
    {
        public int MessageID { get; set; }
        public string PortalMessageUserID { get; set; }
        public string TextMessage { get; set; }
        public string SendDate { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string PhotoPath { get; set; }
        public string TagUserName { get; set; }
        public string TagColor { get; set; }
    }
}
