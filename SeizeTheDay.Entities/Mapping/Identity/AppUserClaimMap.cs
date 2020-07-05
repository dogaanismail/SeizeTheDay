using SeizeTheDay.Core.Domain.Identity;

namespace SeizeTheDay.Entities.Mapping.Identity
{
    public class AppUserClaimMap : SystemEntityTypeConfiguration<AppUserClaim>
    {
        public AppUserClaimMap()
        {
            this.ToTable("AppUserClaim");
            this.HasKey<int>(f => f.Id);
            this.Property(f => f.ClaimType).IsRequired().HasMaxLength(256);
            this.Property(f => f.ClaimValue).IsRequired().HasMaxLength(256);
        }
    }
}
