using SeizeTheDay.Core.Entities;
using System;

namespace SeizeTheDay.Core.Domain.Identity
{
    public class AppUserDetail : BaseEntity
    {
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
        public bool? IsActive { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? RegisteredDate { get; set; }
        public int? InsertBy { get; set; }
        public int? LastModifiedBy { get; set; }
        public string CoverPhotoPath { get; set; }
        public string UserCity { get; set; }
        public int? CountryID { get; set; }
        public int? UserTypeID { get; set; }
        public string UserTask { get; set; }
        public string TagUserName { get; set; }
        public string TagColor { get; set; }
    }
}
