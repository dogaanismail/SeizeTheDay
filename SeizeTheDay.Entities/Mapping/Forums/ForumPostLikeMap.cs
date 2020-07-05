using SeizeTheDay.Core.Domain.Forums;

namespace SeizeTheDay.Entities.Mapping.Forums
{
    public partial class ForumPostLikeMap : SystemEntityTypeConfiguration<ForumPostLike>
    {
        public ForumPostLikeMap()
        {
            this.ToTable("ForumPostLike");
            this.HasKey(fpl => fpl.Id);

            this.HasRequired(fpl => fpl.ForumPost)
               .WithMany()
               .HasForeignKey(fpl => fpl.ForumPostId);

            this.HasRequired(fpl => fpl.User)
               .WithMany()
               .HasForeignKey(fpl => fpl.CreatedBy)
               .WillCascadeOnDelete(false);
        }
    }
}
