using SeizeTheDay.Core.Entities;

namespace SeizeTheDay.Core.Domain.Chats
{
    public partial class Chat : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name
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
        /// Gets or sets the ChatBoxId
        /// </summary>
        public int ChatGroupId { get; set; }

        /// <summary>
        /// Gets the forum
        /// </summary>
        public virtual ChatGroup ChatGroup { get; set; }

        //TODO
        /// <summary>
        /// Gets the user
        /// </summary>
        //public virtual User User { get; set; }

    }
}
