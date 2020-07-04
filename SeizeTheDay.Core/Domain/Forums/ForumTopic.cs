using SeizeTheDay.Core.Entities;

namespace SeizeTheDay.Core.Domain.Forums
{
    public partial class ForumTopic : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the title
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the status
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// Gets or sets the forum identifier
        /// </summary>
        public int ForumId { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets the forum
        /// </summary>
        public virtual Forum Forum { get; set; }

        //TODO
        /// <summary>
        /// Gets the customer
        /// </summary>
        //public virtual User User { get; set; }
    }
}
