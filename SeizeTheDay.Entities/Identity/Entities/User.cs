//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using System;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace SeizeTheDay.Entities.Identity.Entities
//{
//    public class User : IdentityUser
//    {
//        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
//        {
//            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
//            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
//            // Add custom user claims here
//            return userIdentity;
//        }
//        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
//        {
//            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
//            var userIdentity2 = await manager.CreateIdentityAsync(this, authenticationType);
//            // Add custom user claims here
//            return userIdentity2;
//        }

//        public string FirstName { get; set; }
//        public string LastName { get; set; }
//        public DateTime? BirthDate { get; set; }
//        public string Address { get; set; }
//        public string PhotoPath { get; set; }
//        public string FacebookLink { get; set; }
//        public string TwitterLink { get; set; }
//        public string SkypeID { get; set; }
//        public string Status { get; set; }
//        public bool? IsDefault { get; set; }
//        public bool? IsActive { get; set; }
//        public DateTime? LastLoginDate { get; set; }
//        public DateTime? RegisteredDate { get; set; }
//        public int? InsertBy { get; set; }
//        public DateTime? LastModified { get; set; }
//        public int? LastModifiedBy { get; set; }
//        public string CoverPhotoPath { get; set; }
//        public string UserCity { get; set; }
//        public int? CountryID { get; set; }
//        public int? UserTypeID { get; set; }
//        public string UserTask { get; set; }
//        public string TagUserName { get; set; }
//        public string TagColor { get; set; }
//    }
//}
