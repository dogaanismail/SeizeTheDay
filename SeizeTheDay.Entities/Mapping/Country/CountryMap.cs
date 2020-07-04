namespace SeizeTheDay.Entities.Mapping.Country
{
    public partial class CountryMap : SystemEntityTypeConfiguration<Core.Domain.Country.Country>
    {
        public CountryMap()
        {
            this.ToTable("Country");
            this.HasKey(f => f.Id);
            this.Property(f => f.Name).IsRequired().HasMaxLength(50);
        }
    }
}
