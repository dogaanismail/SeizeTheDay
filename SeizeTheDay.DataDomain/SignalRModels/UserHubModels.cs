using System.Collections.Generic;

namespace SeizeTheDay.DataDomain.SignalRModels
{
    public class UserHubModels
    {
        public string UserName { get; set; }
        public HashSet<string> ConnectionIds { get; set; }
        public string UserID { get; set; }
    }
}