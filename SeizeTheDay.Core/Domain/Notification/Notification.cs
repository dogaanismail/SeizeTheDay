using SeizeTheDay.Core.Domain.Identity;
using SeizeTheDay.Core.Entities;

namespace SeizeTheDay.Core.Domain.Notification
{
    public partial class Notification : BaseEntity
    {
        /// <summary>
        /// Gets or sets the notification type
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the notification detail
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Gets or sets the notification title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the notification detail url
        /// </summary>
        public string DetailsUrl { get; set; }

        /// <summary>
        /// Gets or sets the notification isRead
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// Gets or sets the notification sentTo
        /// </summary>
        public int SentTo { get; set; }

        /// <summary>
        /// Gets the user
        /// </summary>
        public virtual AppUser User { get; set; }
    }
}
