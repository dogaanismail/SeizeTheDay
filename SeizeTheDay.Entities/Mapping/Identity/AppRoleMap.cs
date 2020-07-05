using SeizeTheDay.Core.Domain.Identity;

namespace SeizeTheDay.Entities.Mapping.Identity
{
    public class AppRoleMap : SystemEntityTypeConfiguration<AppRole>
    {
        public AppRoleMap()
        {
            this.ToTable("AppRole");
            this.HasKey<int>(f => f.Id);
            this.Property(f => f.Name).IsRequired().HasMaxLength(128);
        }
    }
}
