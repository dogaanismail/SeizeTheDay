using SeizeTheDay.Core.Domain.Identity;
using SeizeTheDay.Core.Entities;

namespace SeizeTheDay.Core.Domain.Forums
{
    public partial class ForumCommentLike : BaseEntity
    {
        /// <summary>
        /// Gets or sets the user identifier
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the comment identifier
        /// </summary>
        public int CommentId { get; set; }

        /// <summary>
        /// Gets the comment
        /// </summary>
        public virtual ForumPostComment ForumPostComment { get; set; }

        /// <summary>
        /// Gets the user that has liked a comment.
        /// </summary>
        public virtual AppUser User { get; set; }
    }
}
