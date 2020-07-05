using SeizeTheDay.Core.Domain.Identity;
using SeizeTheDay.Core.Entities;

namespace SeizeTheDay.Core.Domain.ProfileVisitor
{
    public partial class ProfileVisitor : BaseEntity
    {
        /// <summary>
        /// Gets or sets the user identifier
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier
        /// </summary>
        public int VisitorId { get; set; }

        /// <summary>
        /// Gets the user
        /// </summary>
        public virtual AppUser User { get; set; }

        /// <summary>
        /// Gets the visitor
        /// </summary>
        public virtual AppUser Visitor { get; set; }
    }
}
