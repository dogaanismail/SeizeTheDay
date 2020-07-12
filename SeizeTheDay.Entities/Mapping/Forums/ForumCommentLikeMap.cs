using SeizeTheDay.Core.Domain.Forums;

namespace SeizeTheDay.Entities.Mapping.Forums
{
    public partial class ForumCommentLikeMap : SystemEntityTypeConfiguration<ForumCommentLike>
    {
        public ForumCommentLikeMap()
        {
            this.ToTable("ForumCommentLike");
            this.HasKey(fcl => fcl.Id);
         
            this.HasRequired(fcl => fcl.User)
               .WithMany()
               .HasForeignKey(fcl => fcl.CreatedBy)
               .WillCascadeOnDelete(false);
        }
    }
}
