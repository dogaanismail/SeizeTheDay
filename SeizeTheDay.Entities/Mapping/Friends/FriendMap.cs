using SeizeTheDay.Core.Domain.Friends;

namespace SeizeTheDay.Entities.Mapping.Friends
{
    public partial class FriendMap : SystemEntityTypeConfiguration<Friend>
    {
        public FriendMap()
        {
            this.ToTable("Friend");
            this.HasKey(f => f.Id);
            this.Property(f => f.BecameFriendDate).IsRequired();

            //TODO
            //this.HasRequired(ft => ft.User)
            //   .WithMany()
            //   .HasForeignKey(ft => ft.UserId)
            //   .WillCascadeOnDelete(false);
        }
    }
}
