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
            Property(x => x.Online).HasColumnName("is_online");
            Property(x => x.Name).HasColumnName("name");
            Property(x => x.LastUpdate).HasColumnName("last_update");
            Property(x => x.SchemaVersion).HasColumnName("schema_version");
            Property(x => x.Parameters).HasColumnName("parameters");
            Property(x => x.Version).HasColumnName("version");
        }
    }
}