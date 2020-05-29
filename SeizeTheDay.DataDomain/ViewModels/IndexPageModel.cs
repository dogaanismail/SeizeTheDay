using SeizeTheDay.DataDomain.DTOs;
using System.Collections.Generic;
using IdentityUser = SeizeTheDay.Entities.Identity.Entities.User;

namespace SeizeTheDay.DataDomain.ViewModels
{

    public class IndexPageModel
    {
        public List<TopicDetailDto> ForumPostList { get; set; }
        public IdentityUser LastUser { get; set; }
        public int TotalPost { get; set; }
        public int TotalTopic { get; set; }
        public int TotalReplies { get; set; }
        public int TotalMembers { get; set; }
        public List<IdentityUser> OnlineUsers { get; set; }
        public int OnlineUsersCount { get; set; }
        public List<IdentityUser> OfflineUsers { get; set; }
        public int OfflineUsersCount { get; set; }
        public List<TopicDetailDto> NewPosts { get; set; }
        public List<TopicDetailDto> MostRepliedPost { get; set; }
    }
}