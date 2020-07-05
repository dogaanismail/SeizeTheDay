using SeizeTheDay.Core.Domain.Forums;

namespace SeizeTheDay.Entities.Mapping.Forums
{
    public partial class ForumPostMap : SystemEntityTypeConfiguration<ForumPost>
    {
        public ForumPostMap()
        {
            this.ToTable("ForumPost");
            this.HasKey(fp => fp.Id);
            this.Property(fp => fp.Title).IsRequired().HasMaxLength(256);
            this.Property(fp => fp.Content).IsRequired().HasColumnType("nvarchar(max)");
            this.Property(fp => fp.ShowInPortal).IsOptional();
            this.Property(fp => fp.PostLocked).IsOptional();
            this.Property(fp => fp.ReviewCount).IsOptional();
            this.Property(fp => fp.IsDefault).IsOptional();

            this.HasRequired(fp => fp.Forum)
               .WithMany()
               .HasForeignKey(fp => fp.ForumId)
                 .WillCascadeOnDelete(false);

            this.HasRequired(fp => fp.ForumTopic)
               .WithMany()
               .HasForeignKey(fp => fp.ForumTopicId)
                .WillCascadeOnDelete(false);

            this.HasRequired(fp => fp.User)
              .WithMany()
              .HasForeignKey(fp => fp.CreatedBy)
              .WillCascadeOnDelete(false);
        }
    }
}
