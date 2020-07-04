using SeizeTheDay.Core.Entities;
using System;

namespace SeizeTheDay.Core.Domain.Friends
{
    public partial class Friend : BaseEntity
    {
        /// <summary>
        /// Gets or sets the FutureFriendId
        /// </summary>
        public int FutureFriendId { get; set; }

        /// <summary>
        /// Gets or sets the UserId
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the UserId
        /// </summary>
        public DateTime BecameFriendDate { get; set; }

        //public virtual AppUser FutureFriend { get; set; }
        //public virtual AppUser FriendUser { get; set; }
    }
}
