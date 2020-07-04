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

        //TODO
        /// <summary>
        /// Gets the user
        /// </summary>
        //public virtual User User { get; set; }
    }
}
