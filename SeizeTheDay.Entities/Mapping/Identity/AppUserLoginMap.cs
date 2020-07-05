using SeizeTheDay.Core.Domain.Identity;

namespace SeizeTheDay.Entities.Mapping.Identity
{
    public class AppUserLoginMap : SystemEntityTypeConfiguration<AppUserLogin>
    {
        public AppUserLoginMap()
        {
            this.ToTable("AppUserLogin");
            this.HasKey(f => new { f.LoginProvider, f.ProviderKey });
            this.Property(f => f.LoginProvider).IsRequired().HasMaxLength(256);
            this.Property(f => f.ProviderKey).IsRequired().HasMaxLength(256);
        }
    }
}
