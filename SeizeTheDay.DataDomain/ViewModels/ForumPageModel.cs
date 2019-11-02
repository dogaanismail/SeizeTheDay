using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.DataDomain.ViewModels
{
    public class ForumPageModel
    {
        public List<Forum> ForumList { get; set; }
        public SeizeTheDay.Entities.Identity.Entities.User LastUser { get; set; }
        public int TotalPost { get; set; }
        public int TotalTopic { get; set; }
        public int TotalReplies { get; set; }
        public int TotalMembers { get; set; }

        public List<Entities.Identity.Entities.User> OnlineUsers { get; set; }
        public int OnlineUsersCount { get; set; }

        public List<Entities.Identity.Entities.User> OfflineUsers { get; set; }
        public int OfflineUsersCount { get; set; }

        public List<ForumPost> NewPosts { get; set; }
        public List<ForumPost> MostRepliedPost
        {
            get; set;
        }

    }
}