namespace SeizeTheDay.Entities.Mapping.Module
{
    public partial class ModuleMap : SystemEntityTypeConfiguration<Core.Domain.Module.Module>
    {
        public ModuleMap()
        {
            this.ToTable("Module");
            this.HasKey(m => m.Id);
            this.Property(m => m.Name).IsRequired().HasMaxLength(128);
            this.Property(m => m.PageIcon).IsRequired().HasMaxLength(128);
            this.Property(m => m.PageUrl).IsRequired().HasMaxLength(128);
            this.Property(m => m.PageSlug).IsRequired().HasMaxLength(128);
            this.Property(m => m.DisplayOrder).IsRequired();
            this.Property(m => m.ParentModuleId).IsRequired();
        }
    }
}
