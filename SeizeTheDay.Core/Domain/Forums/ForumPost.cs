using SeizeTheDay.Core.Entities;
using System.Collections.Generic;

namespace SeizeTheDay.Core.Domain.Forums
{
    public partial class ForumPost : BaseEntity
    {
        private ICollection<ForumPostComment> _forumPostComments;

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the show in portal
        /// </summary>
        public bool ShowInPortal { get; set; }

        /// <summary>
        /// Gets or sets the post locked status
        /// </summary>
        public bool PostLocked { get; set; }

        /// <summary>
        /// Gets or sets the review count
        /// </summary>
        public int ReviewCount { get; set; }

        /// <summary>
        /// Gets or sets the isdefault
        /// </summary>
        public int IsDefault { get; set; }

        /// <summary>
        /// Gets or sets the forum identifier
        /// </summary>
        public int ForumId { get; set; }

        /// <summary>
        /// Gets or sets the forum topic identifier
        /// </summary>
        public int ForumTopicId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets the forum
        /// </summary>
        public virtual Forum Forum { get; set; }

        /// <summary>
        /// Gets the forumtopic
        /// </summary>
        public virtual ForumTopic ForumTopic { get; set; }

        /// <summary>
        /// Gets or sets forumpost comments
        /// </summary>
        public virtual ICollection<ForumPostComment> ForumPostComments
        {
            get { return _forumPostComments ?? (_forumPostComments = new List<ForumPostComment>()); }
            protected set { _forumPostComments = value; }
        }

        //TODO
        /// <summary>
        /// Gets the user
        /// </summary>
        //public virtual User User { get; set; }
    }
}
