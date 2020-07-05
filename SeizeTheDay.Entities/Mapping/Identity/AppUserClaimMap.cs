using SeizeTheDay.Core.Domain.Identity;

namespace SeizeTheDay.Entities.Mapping.Identity
{
    public partial class AppUserClaimMap : SystemEntityTypeConfiguration<AppUserClaim>
    {
        public AppUserClaimMap()
        {
            this.ToTable("AppUserClaim");
            this.HasKey(f => f.Id);
        }
    }
}
