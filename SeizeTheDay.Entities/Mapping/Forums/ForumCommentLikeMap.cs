using SeizeTheDay.Core.Domain.Forums;

namespace SeizeTheDay.Entities.Mapping.Forums
{
    public partial class ForumCommentLikeMap : SystemEntityTypeConfiguration<ForumCommentLike>
    {
        public ForumCommentLikeMap()
        {
            this.ToTable("ForumCommentLike");
            this.HasKey(fcl => fcl.Id);

            this.HasRequired(fcl => fcl.ForumPostComment)
               .WithMany()
               .HasForeignKey(fcl => fcl.CommentId);

            //TODO
            //this.HasRequired(ft => ft.User)
            //   .WithMany()
            //   .HasForeignKey(ft => ft.UserId)
            //   .WillCascadeOnDelete(false);
        }
    }
}
