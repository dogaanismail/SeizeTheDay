using SeizeTheDay.Core.Domain.Forums;

namespace SeizeTheDay.Entities.Mapping.Forums
{
    public partial class ForumMap : SystemEntityTypeConfiguration<Forum>
    {
        public ForumMap()
        {
            this.ToTable("Forum");
            this.HasKey(f => f.Id);
            this.Property(f => f.Name).IsRequired().HasMaxLength(200);
            this.Property(f => f.Title).IsRequired().HasMaxLength(200);
            this.Property(f => f.Description).IsRequired().HasMaxLength(500);
            this.Property(f => f.Status).IsRequired().HasMaxLength(25);
            this.Property(f => f.IsDefault).IsRequired().IsOptional();

            //TODO
            //this.HasRequired(ft => ft.User)
            //   .WithMany()
            //   .HasForeignKey(ft => ft.UserId)
            //   .WillCascadeOnDelete(false);
        }
    }
}
