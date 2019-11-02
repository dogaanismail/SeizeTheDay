using System;

namespace SeizeTheDay.Entities.EntityClasses.MySQL
{
    public class UserPermission
    {
        public int ID { get; set; }
        public Nullable<int> ModuleID { get; set; }
        public string ModuleName { get; set; }
        public string RoleID { get; set; }

        public Nullable<int> DisplayOrder { get; set; }
        public string PageIcon { get; set; }
        public string PageUrl { get; set; }
        public string PageSlug { get; set; }
        public Nullable<bool> IsDefault { get; set; }
        public Nullable<bool>   IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> ParentModuleID { get; set; }
    }
}