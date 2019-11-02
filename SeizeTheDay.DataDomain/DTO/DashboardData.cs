using System;
using System.Collections.Generic;

namespace SeizeTheDay.Controllers
{
    public class DashboardData
    {
        public Nullable<int> TotalUsers { get; set; }
        public Nullable<int> NewUsers { get; set; }
        public Nullable<int> UnconfirmedUsers { get; set; }
        public Nullable<int> BannedUsers { get; set; }
        public virtual List<SeizeTheDay.Entities.EntityClasses.MySQL.User> UserMast { get; set; }
        public List<Xgteamc1XgTeamModel.User> UsersMapData { get; set; }

    }
}