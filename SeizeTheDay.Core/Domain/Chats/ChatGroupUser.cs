using SeizeTheDay.Core.Domain.Identity;
using SeizeTheDay.Core.Entities;

namespace SeizeTheDay.Core.Domain.Chats
{
    public class ChatGroupUser : BaseEntity
    {
        /// <summary>
        /// Gets or sets the ChatGroupId
        /// </summary>
        public int ChatGroupId { get; set; }

        /// <summary>
        /// Gets or sets the MemberId
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// Gets the chatgroup
        /// </summary>
        public virtual ChatGroup ChatGroup { get; set; }

        /// <summary>
        /// Gets the GroupMember
        /// </summary>
        public virtual AppUser GroupMember { get; set; }
    }
}
