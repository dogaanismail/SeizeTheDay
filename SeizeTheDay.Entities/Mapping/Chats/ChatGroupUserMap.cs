using SeizeTheDay.Core.Domain.Chats;

namespace SeizeTheDay.Entities.Mapping.Chats
{
    public partial class ChatGroupUserMap : SystemEntityTypeConfiguration<ChatGroupUser>
    {
        public ChatGroupUserMap()
        {
            this.ToTable("ChatGroupUser");
            this.HasKey(c => c.Id);

            this.HasRequired(fp => fp.GroupMember)
                .WithMany()
                .HasForeignKey(fp => fp.MemberId)
            .WillCascadeOnDelete(false);
        }
    }
}
