using SeizeTheDay.Core.Domain.Chats;

namespace SeizeTheDay.Entities.Mapping.Chats
{
    public partial class ChatGroupUserMap : SystemEntityTypeConfiguration<ChatGroupUser>
    {
        public ChatGroupUserMap()
        {
            this.ToTable("ChatGroupUser");
            this.HasKey(c => c.Id);

            this.HasRequired(fp => fp.ChatGroup)
                .WithMany()
                .HasForeignKey(fp => fp.ChatGroupId);

            //TODO
            //this.HasRequired(fp => fp.Member)
            //    .WithMany()
            //    .HasForeignKey(fp => fp.MemberId);
        }
    }
}
