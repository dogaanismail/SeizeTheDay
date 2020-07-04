namespace SeizeTheDay.Entities.Mapping.ProfileVisitor
{
    public partial class ProfileVisitorMap : SystemEntityTypeConfiguration<Core.Domain.ProfileVisitor.ProfileVisitor>
    {
        public ProfileVisitorMap()
        {
            this.ToTable("ProfileVisitor");
            this.HasKey(p => p.Id);

            //TODO
            //this.HasRequired(ft => ft.User)
            //   .WithMany()
            //   .HasForeignKey(ft => ft.UserId)
            //   .WillCascadeOnDelete(false);

            //TODO
            //this.HasRequired(ft => ft.User)
            //   .WithMany()
            //   .HasForeignKey(ft => ft.UserId)
            //   .WillCascadeOnDelete(false);
        }
    }
}
