using SeizeTheDay.Core.Domain.Identity;
using SeizeTheDay.Core.Entities;
using System.Collections.Generic;

namespace SeizeTheDay.Core.Domain.Forums
{
    public partial class ForumPostComment : BaseEntity
    {
        public ForumPostComment()
        {
            PostCommentLikes = new HashSet<ForumCommentLike>();
        }

        /// <summary>
        /// Gets or sets the text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the user identifier
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the forumpost identifier
        /// </summary>
        public int ForumPostId { get; set; }

        /// <summary>
        /// Gets the forumtopic
        /// </summary>
        public virtual ForumPost ForumPost { get; set; }

        /// <summary>
        /// Gets the user that has created a comment.
        /// </summary>
        public virtual AppUser User { get; set; }

        /// <summary>
        /// Gets or sets forumpost comment likes
        /// </summary>
        public virtual ICollection<ForumCommentLike> PostCommentLikes { get; set; }
    }
}
