using SeizeTheDay.Core.Domain.Identity;

namespace SeizeTheDay.Entities.Mapping.Identity
{
    public class AppUserRoleMap : SystemEntityTypeConfiguration<AppUserRole>
    {
        public AppUserRoleMap()
        {
            ToTable("AppUserRole");
            HasKey(f => new { f.RoleId, f.UserId });
        }
    }
}
