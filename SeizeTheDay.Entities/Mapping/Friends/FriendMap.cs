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

            this.HasRequired(f => f.FriendUser)
               .WithMany()
               .HasForeignKey(f => f.UserId)
               .WillCascadeOnDelete(false);

            this.HasRequired(f => f.FutureFriend)
               .WithMany()
               .HasForeignKey(f => f.FutureFriendId)
               .WillCascadeOnDelete(false);
        }
    }
}
