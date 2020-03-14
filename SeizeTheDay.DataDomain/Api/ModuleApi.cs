namespace SeizeTheDay.DataDomain.Api
{
    public class ModuleApi
    {
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public int? DisplayOrder { get; set; }
        public string PageIcon { get; set; }
        public string PageUrl { get; set; }
        public string PageSlug { get; set; }
        public bool? IsDefault { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? ParentModuleId { get; set; }
    }
}
