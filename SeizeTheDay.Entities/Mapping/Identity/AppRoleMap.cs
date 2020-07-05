using SeizeTheDay.Core.Domain.Identity;

namespace SeizeTheDay.Entities.Mapping.Identity
{
    public partial class AppRoleMap : SystemEntityTypeConfiguration<AppRole>
    {
        public AppRoleMap()
        {
            this.ToTable("AppRole");
            this.HasKey(f => f.Id);
        }
    }
}
