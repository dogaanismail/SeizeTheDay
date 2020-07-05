using SeizeTheDay.Core.Entities;

namespace SeizeTheDay.Core.Domain.Module
{
    public partial class Module : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the page icon
        /// </summary>
        public string PageIcon { get; set; }

        /// <summary>
        /// Gets or sets the page url
        /// </summary>
        public string PageUrl { get; set; }

        /// <summary>
        /// Gets or sets the page slug
        /// </summary>
        public string PageSlug { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets or sets the parentmoduleId
        /// </summary>
        public int ParentModuleId { get; set; }
    }
}
