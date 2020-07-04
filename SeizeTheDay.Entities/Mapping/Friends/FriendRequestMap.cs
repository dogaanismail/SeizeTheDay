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
            this.Property(fr => fr.RequestMessage).IsOptional();

            //TODO
            //this.HasRequired(ft => ft.User)
            //   .WithMany()
            //   .HasForeignKey(ft => ft.UserId)
            //   .WillCascadeOnDelete(false);
        }
    }
}
