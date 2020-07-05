using SeizeTheDay.Core.Domain.Identity;

namespace SeizeTheDay.Entities.Mapping.Identity
{
    public partial class AppUserLoginMap : SystemEntityTypeConfiguration<AppUserLogin>
    {
        public AppUserLoginMap()
        {
            this.ToTable("AppUserLogin");
            this.HasKey(f => f.LoginProvider);
            this.HasKey(f => f.ProviderKey);
        }
    }
}
