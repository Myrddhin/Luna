using System;
using System.Data.Entity.ModelConfiguration;
using Luna.Model.Configuration;

namespace Luna.Data.Local.SQLite.TablesConfiguration
{
    internal class ExternalAccountConfiguration : EntityTypeConfiguration<ExternalAccount>
    {
        public ExternalAccountConfiguration()
        {
            ToTable("ExternalAccounts");
            HasKey<Guid>(x => x.Id);
            Property(x => x.ProviderType).HasColumnName("provider_type");
            Property(x => x.Parameters).HasColumnName("parameters");
            Property(x => x.Geolocalizer).HasColumnName("geolocalizer");
            Property(x => x.Contacts).HasColumnName("contacts");
        }
    }
}