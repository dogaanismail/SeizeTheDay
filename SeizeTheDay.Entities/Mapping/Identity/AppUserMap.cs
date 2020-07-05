using SeizeTheDay.Core.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeizeTheDay.Entities.Mapping.Identity
{
    public partial class AppUserMap : SystemEntityTypeConfiguration<AppUser>
    {
        public AppUserMap()
        {
            this.ToTable("AppUser");
            this.HasKey(f => f.Id);

            //TODO
            //this.HasRequired(ft => ft.User)
            //   .WithMany()
            //   .HasForeignKey(ft => ft.UserId)
            //   .WillCascadeOnDelete(false);
        }
    }
}
