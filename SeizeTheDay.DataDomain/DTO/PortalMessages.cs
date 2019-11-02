using System;

namespace SeizeTheDay.Entities.EntityClasses.MySQL
{
    public class PortalMessages
    {
        //For Messages
        public int? MessageID { get; set; }
        public string PortalMessageUserID { get; set; }
        public string TextMessage { get; set; }
        public string SendDate { get; set; }

        //For Users
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string PhotoPath { get; set; }
        public string TagUserName { get; set; }
        public string TagColor { get; set; }


    }
}
