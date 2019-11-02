using System;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.Entities.EntityClasses.MySQL

{
    public class UserLog
    {
        public int ID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string Message { get; set; }
        public Nullable<System.DateTime> LogTime { get; set; }
        public string IPAddress { get; set; }
        public string MoreInfo { get; set; }

        //public virtual TblUserMast tblUserMast { get; set; }
    }
}