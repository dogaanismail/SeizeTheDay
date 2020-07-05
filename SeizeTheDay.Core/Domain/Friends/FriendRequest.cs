using SeizeTheDay.Core.Domain.Identity;
using SeizeTheDay.Core.Entities;

namespace SeizeTheDay.Core.Domain.Friends
{
    public partial class FriendRequest : BaseEntity
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
        /// Gets or sets the IsAccepted
        /// </summary>
        public bool IsAccepted { get; set; }

        /// <summary>
        /// Gets or sets the IsPending
        /// </summary>
        public bool IsPending { get; set; }

        /// <summary>
        /// Gets or sets the IsRejected
        /// </summary>
        public bool IsRejected { get; set; }

        /// <summary>
        /// Gets or sets the RequestMessage
        /// </summary>
        public string RequestMessage { get; set; }

        /// <summary>
        /// Gets the user that has created a friendrequest.
        /// </summary>
        public virtual AppUser User { get; set; }

        /// <summary>
        /// Gets the user that has been sent a friend request by a user
        /// </summary>
        public virtual AppUser FutureFriend { get; set; }
    }
}
