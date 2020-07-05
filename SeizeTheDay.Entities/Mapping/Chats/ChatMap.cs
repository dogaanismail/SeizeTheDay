namespace SeizeTheDay.Entities.Mapping.Chats
{
    public partial class ChatMap : SystemEntityTypeConfiguration<Core.Domain.Chats.Chat>
    {
        public ChatMap()
        {
            this.ToTable("Chat");
            this.HasKey(c => c.Id);
            this.Property(c => c.Text).IsRequired().HasMaxLength(256);
            this.Property(c => c.IsRead).IsRequired();

            this.HasRequired(fp => fp.ChatGroup)
                .WithMany()
                .HasForeignKey(fp => fp.ChatGroupId);

            this.HasRequired(ft => ft.User)
               .WithMany()
               .HasForeignKey(ft => ft.SenderId)
               .WillCascadeOnDelete(false);
        }
    }
}
