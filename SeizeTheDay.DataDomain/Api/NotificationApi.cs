using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeizeTheDay.DataDomain.Api
{
    public class NotificationApi
    {
        public int NotificationID { get; set; }
        public int? Type { get; set; }
        public string Details { get; set; }
        public string Title { get; set; }
        public string DetailsUrl { get; set; }
        public string SentTo { get; set; }
        public string CreatedDate { get; set; }
        public bool? IsRead { get; set; }
    }
}
