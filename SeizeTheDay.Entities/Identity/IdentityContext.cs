//using Microsoft.AspNet.Identity.EntityFramework;
//using SeizeTheDay.Entities.Identity.Entities;
//using System.Data.Entity;
//using System.Data.Entity.Migrations;

//namespace SeizeTheDay.Entities.Identity
//{
//    public class IdentityContext : IdentityDbContext<User>
//    {
//        public IdentityContext()
//            : base("IdentityConnection", throwIfV1Schema: false)
//        {
//        }
//        public static IdentityContext Create()
//        {
//            return new IdentityContext();
//        }
//        static IdentityContext()
//        {
//            Database.SetInitializer<IdentityContext>(null);
//        }

//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            modelBuilder.Entity<User>().ToTable("UserInfoes");
//            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
//            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
//            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
//            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
//            modelBuilder.Entity<IdentityUser>().ToTable("Users");

//            modelBuilder
//                .Entity<IdentityRole>()
//                .Property(p => p.Name)
//                .HasMaxLength(255);

//            modelBuilder
//                .Entity<IdentityUser>()
//                .Property(p => p.UserName)
//                .HasMaxLength(256);

//            modelBuilder
//              .Properties()
//              .Where(p => p.PropertyType == typeof(string) &&
//                          !p.Name.Contains("Id") &&
//                          !p.Name.Contains("Provider"))
//              .Configure(p => p.HasMaxLength(255));

//        }
//    }
    
//    internal class Configuration : DbMigrationsConfiguration<IdentityContext>
//    {
//        public Configuration()
//        {
//            AutomaticMigrationsEnabled = true;
//            SetSqlGenerator("Devart.Data.MySql", new Devart.Data.MySql.Entity.Migrations.MySqlEntityMigrationSqlGenerator());
//        }
//    }

//}
