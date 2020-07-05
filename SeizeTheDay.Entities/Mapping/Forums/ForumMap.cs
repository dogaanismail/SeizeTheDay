using SeizeTheDay.Core.Domain.Forums;

namespace SeizeTheDay.Entities.Mapping.Forums
{
    public partial class ForumMap : SystemEntityTypeConfiguration<Forum>
    {
        public ForumMap()
        {
            this.ToTable("Forum");
            this.HasKey(f => f.Id);
            this.Property(f => f.Name).IsRequired().HasMaxLength(128);
            this.Property(f => f.Title).IsRequired().HasMaxLength(256);
            this.Property(f => f.Description).IsRequired().HasMaxLength(512);
            this.Property(f => f.Status).IsRequired().HasMaxLength(64);
            this.Property(f => f.IsDefault).IsRequired().IsOptional();

            this.HasRequired(f => f.User)
               .WithMany()
               .HasForeignKey(f => f.CreatedBy)
               .WillCascadeOnDelete(false);
        }
    }
}
