using SeizeTheDay.Core.Domain.Forums;

namespace SeizeTheDay.Entities.Mapping.Forums
{
    public partial class ForumTopicMap : SystemEntityTypeConfiguration<ForumTopic>
    {
        public ForumTopicMap()
        {
            this.ToTable("ForumTopic");
            this.HasKey(ft => ft.Id);
            this.Property(ft => ft.Name).IsRequired().HasMaxLength(200);
            this.Property(ft => ft.Title).IsRequired().HasMaxLength(200);
            this.Property(ft => ft.Description).IsRequired().HasMaxLength(500);
            this.Property(ft => ft.IsDefault).IsRequired().IsOptional();

            this.HasRequired(ft => ft.Forum)
                .WithMany()
                .HasForeignKey(ft => ft.ForumId);

            //TODO
            //this.HasRequired(ft => ft.Customer)
            //   .WithMany()
            //   .HasForeignKey(ft => ft.CustomerId)
            //   .WillCascadeOnDelete(false);
        }
    }
}
