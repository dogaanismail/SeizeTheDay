namespace SeizeTheDay.Entities.Mapping.Setting
{
    public partial class SettingMap : SystemEntityTypeConfiguration<Core.Domain.Setting.Setting>
    {
        public SettingMap()
        {
            this.ToTable("Setting");
            this.HasKey(s => s.Id);
            this.Property(s => s.Name).IsRequired().HasMaxLength(200);
            this.Property(s => s.Value).IsRequired();
        }
    }
}
