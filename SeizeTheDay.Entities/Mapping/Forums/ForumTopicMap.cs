using SeizeTheDay.Core.Domain.Forums;

namespace SeizeTheDay.Entities.Mapping.Forums
{
    public partial class ForumTopicMap : SystemEntityTypeConfiguration<ForumTopic>
    {
        public ForumTopicMap()
        {
            this.ToTable("ForumTopic");
            this.HasKey(ft => ft.Id);
            this.Property(ft => ft.Name).IsRequired().HasMaxLength(256);
            this.Property(ft => ft.Title).IsRequired().HasMaxLength(256);
            this.Property(ft => ft.Description).IsRequired().HasMaxLength(512);
            this.Property(ft => ft.IsDefault).IsRequired().IsOptional();

            this.HasRequired(ft => ft.Forum)
                .WithMany()
                .HasForeignKey(ft => ft.ForumId)
                .WillCascadeOnDelete(false);

            this.HasRequired(ft => ft.User)
               .WithMany()
               .HasForeignKey(ft => ft.CreatedBy)
               .WillCascadeOnDelete(false);
        }
    }
}
