using System.Data.Entity.ModelConfiguration;
using Luna.Model.Storage;

namespace Luna.Data.Local.SQLite.TablesConfiguration
{
    internal class RepositorySettingsConfiguration : EntityTypeConfiguration<RepositorySettings>
    {
        public RepositorySettingsConfiguration()
        {
            ToTable("RepositorySettings");
            HasKey(x => x.Id);
            Property(x => x.Name).HasColumnName("name");
            Property(x => x.Default).HasColumnName("is_default");
            Property(x => x.SchemaVersion).HasColumnName("schema_version");
            Property(x => x.Online).HasColumnName("online");
            Property(x => x.LastUsed).HasColumnName("last_used");
        }
    }
}