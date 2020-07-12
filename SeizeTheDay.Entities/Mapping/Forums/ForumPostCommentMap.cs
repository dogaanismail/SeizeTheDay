using SeizeTheDay.Core.Domain.Forums;

namespace SeizeTheDay.Entities.Mapping.Forums
{
    public partial class ForumPostCommentMap : SystemEntityTypeConfiguration<ForumPostComment>
    {
        public ForumPostCommentMap()
        {
            this.ToTable("ForumPostComment");
            this.HasKey(fpm => fpm.Id);
            this.Property(fpm => fpm.Text).IsRequired().HasMaxLength(512);

            this.HasRequired(fpm => fpm.User)
               .WithMany()
               .HasForeignKey(fpm => fpm.CreatedBy)
               .WillCascadeOnDelete(false);
        }
    }
}
