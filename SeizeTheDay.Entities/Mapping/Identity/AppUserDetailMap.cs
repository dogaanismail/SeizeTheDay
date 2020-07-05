using SeizeTheDay.Core.Domain.Identity;

namespace SeizeTheDay.Entities.Mapping.Identity
{
    public class AppUserDetailMap : SystemEntityTypeConfiguration<AppUserDetail>
    {
        public AppUserDetailMap()
        {
            this.ToTable("AppUserDetail");
            this.HasKey<int>(aud => aud.Id);
            this.Property(aud => aud.FirstName).IsRequired().HasMaxLength(128);
            this.Property(aud => aud.LastName).IsRequired().HasMaxLength(128);
            this.Property(aud => aud.BirthDate).IsOptional();
            this.Property(aud => aud.Address).IsOptional().HasMaxLength(512);
            this.Property(aud => aud.PhotoPath).IsRequired().HasMaxLength(256);
            this.Property(aud => aud.FacebookLink).IsOptional().HasMaxLength(256);
            this.Property(aud => aud.TwitterLink).IsOptional().HasMaxLength(256);
            this.Property(aud => aud.SkypeID).IsOptional().HasMaxLength(256);
            this.Property(aud => aud.Status).IsOptional().HasMaxLength(512);
            this.Property(aud => aud.IsDefault).IsOptional();
            this.Property(aud => aud.IsActive).IsOptional();
            this.Property(aud => aud.LastLoginDate).IsOptional();
            this.Property(aud => aud.RegisteredDate).IsOptional();
            this.Property(aud => aud.InsertBy).IsOptional();
            this.Property(aud => aud.LastModifiedBy).IsOptional();
            this.Property(aud => aud.CoverPhotoPath).IsRequired().HasMaxLength(256);
            this.Property(aud => aud.UserCity).IsRequired().HasMaxLength(32);
            this.Property(aud => aud.CountryID).IsOptional();
            this.Property(aud => aud.UserTypeID).IsOptional();
            this.Property(aud => aud.UserTask).IsOptional().HasMaxLength(64);
            this.Property(aud => aud.TagUserName).IsOptional().HasMaxLength(64);
            this.Property(aud => aud.TagColor).IsOptional().HasMaxLength(64);

            //this.HasRequired(ft => ft.CountryID)
            //   .WithMany()
            //   .HasForeignKey(ft => ft.UserId)
            //   .WillCascadeOnDelete(false);
        }
    }
}
