using System;
using System.Data.Entity.ModelConfiguration;
using Luna.Model.Storage;

namespace Luna.Data.Local.SQLite.TablesConfiguration
{
    internal class RepositoryConfiguration : EntityTypeConfiguration<Repository>
    {
        public RepositoryConfiguration()
        {
            ToTable("Repositories");
            HasKey<Guid>(x => x.Id);
            Property(x => x.ApiKey).HasColumnName("api_key");
            Property(x => x.IsDefault).HasColumnName("is_default");
            Property(x => x.IsOnline).HasColumnName("is_online");
            Property(x => x.SchemaVersion).HasColumnName("schema_version");
        }
    }
}