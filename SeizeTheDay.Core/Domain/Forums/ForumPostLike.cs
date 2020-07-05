using SeizeTheDay.Core.Domain.Identity;
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

        /// <summary>
        /// Gets the user that has liked a post.
        /// </summary>
        public virtual AppUser User { get; set; }
    }
}
