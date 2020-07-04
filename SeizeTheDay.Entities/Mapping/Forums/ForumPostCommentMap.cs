using SeizeTheDay.Core.Domain.Forums;

namespace SeizeTheDay.Entities.Mapping.Forums
{
    public partial class ForumPostCommentMap : SystemEntityTypeConfiguration<ForumPostComment>
    {
        public ForumPostCommentMap()
        {
            this.ToTable("ForumPostComment");
            this.HasKey(fpm => fpm.Id);
            this.Property(fpm => fpm.Text).IsRequired().HasMaxLength(400);

            this.HasRequired(fpm => fpm.ForumPost)
                .WithMany()
                .HasForeignKey(fpm => fpm.ForumPostId);

            //TODO
            //this.HasRequired(ft => ft.User)
            //   .WithMany()
            //   .HasForeignKey(ft => ft.UserId)
            //   .WillCascadeOnDelete(false);
        }
    }
}
