using SeizeTheDay.Core.Domain.Identity;
using SeizeTheDay.DataDomain.DTOs;
using System.Collections.Generic;
using Xgteamc1XgTeamModel;

namespace SeizeTheDay.DataDomain.ViewModels
{
    public class ForumPageModel
    {
        public List<Forum> ForumList { get; set; }
        public AppUser LastUser { get; set; }
        public int TotalPost { get; set; }
        public int TotalTopic { get; set; }
        public int TotalReplies { get; set; }
        public int TotalMembers { get; set; }
        public List<AppUser> OnlineUsers { get; set; }
        public int OnlineUsersCount { get; set; }
        public List<AppUser> OfflineUsers { get; set; }
        public int OfflineUsersCount { get; set; }
        public List<TopicDetailDto> NewPosts { get; set; }
        public List<TopicDetailDto> MostRepliedPost { get; set; }

    }
}