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

        //TODO
        /// <summary>
        /// Gets the customer
        /// </summary>
        //public virtual User User { get; set; }
    }
}
