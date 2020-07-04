using SeizeTheDay.Core.Entities;
using System.Collections.Generic;

namespace SeizeTheDay.Core.Domain.Chats
{
    public partial class ChatGroup : BaseEntity
    {
        private ICollection<ChatGroupUser> _chatGroupUsers;
        private ICollection<Chat> _chats;

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
        public virtual ICollection<ChatGroupUser> ChatGroupMembers
        {
            get { return _chatGroupUsers ?? (_chatGroupUsers = new List<ChatGroupUser>()); }
            protected set { _chatGroupUsers = value; }
        }

        /// <summary>
        /// Gets or sets chatgroup chats
        /// </summary>
        public virtual ICollection<Chat> Chats
        {
            get { return _chats ?? (_chats = new List<Chat>()); }
            protected set { _chats = value; }
        }

    }
}
