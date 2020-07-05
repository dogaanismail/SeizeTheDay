using SeizeTheDay.Core.Domain.Identity;

namespace SeizeTheDay.Entities.Mapping.Identity
{
    public partial class AppUserMap : SystemEntityTypeConfiguration<AppUser>
    {
        public AppUserMap()
        {
            this.ToTable("AppUser");
            this.HasKey(f => f.Id);

            this.HasRequired(ft => ft.UserDetail)
               .WithMany()
               .HasForeignKey(ft => ft.UserDetailId)
               .WillCascadeOnDelete(false);
        }
    }
}
