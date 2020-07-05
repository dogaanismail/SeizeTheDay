using SeizeTheDay.Core.Domain.Identity;

namespace SeizeTheDay.Entities.Mapping.Identity
{
    public partial class AppUserRoleMap : SystemEntityTypeConfiguration<AppUserRole>
    {
        public AppUserRoleMap()
        {
            this.ToTable("AppUserRole");
            this.HasKey(f => f.RoleId);
            this.HasKey(f => f.UserId);
        }
    }
}
