using SeizeTheDay.Core.Entities;

namespace SeizeTheDay.Core.Domain.Forums
{
    public partial class ForumPostLike : BaseEntity
    {
        /// <summary>
        /// Gets or sets the user identifier
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the forum post identifier
        /// </summary>
        public int ForumPostId { get; set; }

        /// <summary>
        /// Gets the comment
        /// </summary>
        public virtual ForumPost ForumPost { get; set; }

        //TODO
        /// <summary>
        /// Gets the customer
        /// </summary>
        //public virtual User User { get; set; }
    }
}
