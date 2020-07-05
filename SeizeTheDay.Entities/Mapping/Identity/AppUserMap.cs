using SeizeTheDay.Core.Domain.Identity;

namespace SeizeTheDay.Entities.Mapping.Identity
{
    public class AppUserMap : SystemEntityTypeConfiguration<AppUser>
    {
        public AppUserMap()
        {
            this.ToTable("AppUser");
            this.HasKey<int>(f => f.Id);
            this.Property(aud => aud.PhoneNumber).IsOptional().HasMaxLength(32);

            this.HasRequired(ft => ft.UserDetail)
             .WithMany()
             .HasForeignKey(ft => ft.UserDetailId)
             .WillCascadeOnDelete(false);
        }
    }
}
