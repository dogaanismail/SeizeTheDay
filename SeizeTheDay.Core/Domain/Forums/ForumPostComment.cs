﻿using SeizeTheDay.Core.Entities;

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
        /// Gets or sets the forum identifier
        /// </summary>
        public int ForumPostId { get; set; }

        /// <summary>
        /// Gets the forumtopic
        /// </summary>
        public virtual ForumPost ForumPost { get; set; }

        //TODO
        /// <summary>
        /// Gets the user
        /// </summary>
        //public virtual User User { get; set; }
    }

}