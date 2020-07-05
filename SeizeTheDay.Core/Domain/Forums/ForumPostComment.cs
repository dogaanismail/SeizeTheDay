using SeizeTheDay.Core.Domain.Identity;
using SeizeTheDay.Core.Entities;

namespace SeizeTheDay.Core.Domain.Forums
{
    public partial class ForumPostComment : BaseEntity
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
    }
}
