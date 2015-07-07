using System.Data.Entity.ModelConfiguration;
using Luna.Model.CRM;

namespace Luna.Data.Local.SQLite.TablesConfiguration
{
    internal class AddressConfiguration : EntityTypeConfiguration<Address>
    {
        public AddressConfiguration()
        {
            ToTable("Addresses");
            HasKey(x => x.Id);
            Property(x => x.Name).HasColumnName("address");
            Property(x => x.ZipCode).HasColumnName("zipcode");
            Property(x => x.City).HasColumnName("city");
            Property(x => x.Country).HasColumnName("country");
        }
    }
}