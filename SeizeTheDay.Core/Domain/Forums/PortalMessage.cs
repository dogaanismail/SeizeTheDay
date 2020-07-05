using SeizeTheDay.Core.Domain.Identity;
using SeizeTheDay.Core.Entities;

namespace SeizeTheDay.Core.Domain.Forums
{
    public partial class PortalMessage : BaseEntity
    {
        /// <summary>
        /// Gets or sets the text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the user identifier
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets the user that has created a portalmessage.
        /// </summary>
        public virtual AppUser User { get; set; }
    }
}
