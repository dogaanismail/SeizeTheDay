using SeizeTheDay.Core.Domain.Identity;

namespace SeizeTheDay.Entities.Mapping.Identity
{
    public class AppUserDetailMap : SystemEntityTypeConfiguration<AppUserDetail>
    {
        public AppUserDetailMap()
        {
            this.ToTable("AppUserDetail");
            this.HasKey(aud => aud.Id);
            this.Property(aud => aud.FirstName).IsRequired().HasMaxLength(100);
            this.Property(aud => aud.LastName).IsRequired().HasMaxLength(100);
            this.Property(aud => aud.BirthDate).IsOptional();
            this.Property(aud => aud.Address).IsOptional().HasMaxLength(400);
            this.Property(aud => aud.PhotoPath).IsRequired().HasMaxLength(200);
            this.Property(aud => aud.FacebookLink).IsOptional().HasMaxLength(200);
            this.Property(aud => aud.TwitterLink).IsOptional().HasMaxLength(200);
            this.Property(aud => aud.SkypeID).IsOptional().HasMaxLength(200);
            this.Property(aud => aud.Status).IsOptional().HasMaxLength(50);
            this.Property(aud => aud.IsDefault).IsOptional();
            this.Property(aud => aud.IsActive).IsOptional();
            this.Property(aud => aud.LastLoginDate).IsOptional();
            this.Property(aud => aud.RegisteredDate).IsOptional();
            this.Property(aud => aud.InsertBy).IsOptional();
            this.Property(aud => aud.LastModifiedBy).IsOptional();
            this.Property(aud => aud.CoverPhotoPath).IsRequired().HasMaxLength(150);
            this.Property(aud => aud.UserCity).IsRequired().HasMaxLength(50);
            this.Property(aud => aud.CountryID).IsOptional();
            this.Property(aud => aud.UserTypeID).IsOptional();
            this.Property(aud => aud.UserTask).IsOptional();
            this.Property(aud => aud.TagUserName).IsOptional();
            this.Property(aud => aud.TagColor).IsOptional();

            //this.HasRequired(ft => ft.CountryID)
            //   .WithMany()
            //   .HasForeignKey(ft => ft.UserId)
            //   .WillCascadeOnDelete(false);
        }
    }
}
