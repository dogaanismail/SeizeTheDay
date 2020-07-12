using SeizeTheDay.Core.Entities;
using System.Collections.Generic;

namespace SeizeTheDay.Core.Domain.Chats
{
    public partial class ChatGroup : BaseEntity
    {
        public ChatGroup()
        {
            Chats = new HashSet<Chat>();
            ChatGroupMembers = new HashSet<ChatGroupUser>();
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the group flag
        /// </summary>
        public string GroupFlag { get; set; }

        /// <summary>
        /// Gets or sets chatgroup members
        /// </summary>
        public virtual ICollection<ChatGroupUser> ChatGroupMembers { get; set; }

        /// <summary>
        /// Gets or sets chatgroup chats
        /// </summary>
        public virtual ICollection<Chat> Chats { get; set; }

    }
}
