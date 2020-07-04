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

        //TODO
        /// <summary>
        /// Gets the user
        /// </summary>
        //public virtual User User { get; set; }

        //TODO
        /// <summary>
        /// Gets the user
        /// </summary>
        //public virtual User User { get; set; }
    }
}
