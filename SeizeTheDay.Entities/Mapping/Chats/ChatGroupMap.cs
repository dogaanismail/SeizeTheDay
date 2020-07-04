using SeizeTheDay.Core.Domain.Chats;

namespace SeizeTheDay.Entities.Mapping.Chats
{
    public partial class ChatGroupMap : SystemEntityTypeConfiguration<ChatGroup>
    {
        public ChatGroupMap()
        {
            this.ToTable("ChatGroup");
            this.HasKey(cg => cg.Id);
            this.Property(cg => cg.GroupFlag).IsOptional().HasMaxLength(200);

            //TODO
            //this.HasRequired(ft => ft.User)
            //   .WithMany()
            //   .HasForeignKey(ft => ft.UserId)
            //   .WillCascadeOnDelete(false);
        }
    }
}
