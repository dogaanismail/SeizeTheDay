using SeizeTheDay.Core.Domain.Forums;

namespace SeizeTheDay.Entities.Mapping.Forums
{
    public partial class PortalMessageMap : SystemEntityTypeConfiguration<PortalMessage>
    {
        public PortalMessageMap()
        {
            this.ToTable("PortalMessage");
            this.HasKey(pm => pm.Id);
            this.Property(pm => pm.Text).IsRequired().HasMaxLength(300);
            //TODO
            //this.HasRequired(fp => fp.UserId)
            //  .WithMany()
            //  .HasForeignKey(fp => fp.CustomerId)
            //  .WillCascadeOnDelete(false);
        }
    }
}
