namespace SeizeTheDay.Entities.Mapping.Notification
{
    public partial class NotificationMap : SystemEntityTypeConfiguration<Core.Domain.Notification.Notification>
    {
        public NotificationMap()
        {
            this.ToTable("Notification");
            this.HasKey(n => n.Id);
            this.Property(n => n.Type).IsRequired();
            this.Property(n => n.Details).IsRequired().HasMaxLength(128);
            this.Property(n => n.DetailsUrl).IsRequired().HasMaxLength(128);
            this.Property(n => n.Title).IsOptional().HasMaxLength(256);
            this.Property(n => n.IsRead).IsOptional(); //Todo must be IsRequired
            this.Property(n => n.SentTo).IsOptional(); //Todo must be IsRequired

            this.HasRequired(n => n.User)
               .WithMany()
               .HasForeignKey(n => n.SentTo)
               .WillCascadeOnDelete(false);
        }
    }
}
