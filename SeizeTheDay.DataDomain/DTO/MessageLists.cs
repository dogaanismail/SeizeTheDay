using System;

namespace SeizeTheDay.Entities.EntityClasses.MySQL
{
    public class MessageLists
    {
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public string PhotoPath { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
