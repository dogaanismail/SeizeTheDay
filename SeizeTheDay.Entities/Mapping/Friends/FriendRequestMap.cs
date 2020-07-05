using SeizeTheDay.Core.Domain.Friends;

namespace SeizeTheDay.Entities.Mapping.Friends
{
    public partial class FriendRequestMap : SystemEntityTypeConfiguration<FriendRequest>
    {
        public FriendRequestMap()
        {
            this.ToTable("FriendRequest");
            this.HasKey(fr => fr.Id);
            this.Property(fr => fr.IsAccepted).IsRequired();
            this.Property(fr => fr.IsPending).IsRequired();
            this.Property(fr => fr.IsRejected).IsRequired();
            this.Property(fr => fr.RequestMessage).IsOptional().HasMaxLength(256);

            this.HasRequired(fr => fr.User)
               .WithMany()
               .HasForeignKey(fr => fr.UserId)
               .WillCascadeOnDelete(false);

            this.HasRequired(fr => fr.FutureFriend)
              .WithMany()
              .HasForeignKey(fr => fr.FutureFriendId)
              .WillCascadeOnDelete(false);
        }
    }
}
