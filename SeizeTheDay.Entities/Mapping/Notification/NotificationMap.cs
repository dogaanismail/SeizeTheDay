namespace SeizeTheDay.Entities.Mapping.Notification
{
    public partial class NotificationMap : SystemEntityTypeConfiguration<Core.Domain.Notification.Notification>
    {
        public NotificationMap()
        {
            this.ToTable("Notification");
            this.HasKey(n => n.Id);
            this.Property(n => n.Type).IsRequired();
            this.Property(n => n.Details).IsRequired();
            this.Property(n => n.DetailsUrl).IsRequired();
            this.Property(n => n.Title).IsOptional();
            this.Property(n => n.IsRead).IsOptional(); //Todo must be IsRequired
            this.Property(n => n.SentTo).IsOptional(); //Todo must be IsRequired

            //TODO
            //this.HasRequired(ft => ft.User)
            //   .WithMany()
            //   .HasForeignKey(ft => ft.UserId)
            //   .WillCascadeOnDelete(false);
        }
    }
}
