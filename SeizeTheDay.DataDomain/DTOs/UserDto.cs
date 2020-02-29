using System;
using System.Collections.Generic;

namespace SeizeTheDay.DataDomain.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool? EmaiLConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }
        public bool? TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool? LockoutEnabled { get; set; }
        public int? AccessFailedCount { get; set; }
        public string UserName { get; set; }

        //User Infoes
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public string PhotoPath { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string SkypeID { get; set; }
        public string Status { get; set; }
        public bool? IsDefault { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? RegisteredDate { get; set; }
        public string InsertBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string LastModifiedBy { get; set; }
        public bool? IsActive { get; set; }
        public string CoverPhotoPath { get; set; }
        public string UserCity { get; set; }
        public int? CountryID { get; set; }
        public int? UserTypeID { get; set; }
        public string UserTask { get; set; }
        public string UserTypeName { get; set; }
        public string TagUserName { get; set; }
        public string TagColor { get; set; }
        public string CPassword { get; set; }

        //UserRole
        public List<string> RoleID { get; set; }
        public List<string> RoleName { get; set; }
    }
}
