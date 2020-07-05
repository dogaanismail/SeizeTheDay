using SeizeTheDay.Core.Domain.Identity;
using SeizeTheDay.Core.Entities;

namespace SeizeTheDay.Core.Domain.Chats
{
    public partial class Chat : BaseEntity
    {
        /// <summary>
        /// Gets or sets the text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the SenderId
        /// </summary>
        public int SenderId { get; set; }

        /// <summary>
        /// Gets or sets the IsRead
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// Gets or sets the ChatGroupId
        /// </summary>
        public int ChatGroupId { get; set; }

        /// <summary>
        /// Gets the ChatGroup
        /// </summary>
        public virtual ChatGroup ChatGroup { get; set; }

        /// <summary>
        /// Gets the Sender
        /// </summary>
        public virtual AppUser User { get; set; }

    }
}
