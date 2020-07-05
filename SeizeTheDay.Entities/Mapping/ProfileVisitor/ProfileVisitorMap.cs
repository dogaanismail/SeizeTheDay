namespace SeizeTheDay.Entities.Mapping.ProfileVisitor
{
    public partial class ProfileVisitorMap : SystemEntityTypeConfiguration<Core.Domain.ProfileVisitor.ProfileVisitor>
    {
        public ProfileVisitorMap()
        {
            this.ToTable("ProfileVisitor");
            this.HasKey(p => p.Id);

            this.HasRequired(p => p.User)
               .WithMany()
               .HasForeignKey(p => p.UserId)
               .WillCascadeOnDelete(false);

            this.HasRequired(p => p.Visitor)
               .WithMany()
               .HasForeignKey(p => p.VisitorId)
               .WillCascadeOnDelete(false);
        }
    }
}
