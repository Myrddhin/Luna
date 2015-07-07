using System.Data.Entity.ModelConfiguration;
using Luna.Model.Cloud.Meta;

namespace Luna.Cloud.Data
{
    public class RepositoryTableConfiguration : EntityTypeConfiguration<Repository>
    {
        public RepositoryTableConfiguration()
        {
            ToTable("meta.Repositories");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("id_repository");
            Property(x => x.LastUpdate).HasColumnName("last_update");
            Property(x => x.Name).HasColumnName("name");
            Property(x => x.Parameters).HasColumnName("params");
            Property(x => x.SchemaVersion).HasColumnName("schema_version");
            Property(x => x.SubscriptionId).HasColumnName("id_subscription");
            Property(x => x.Version).HasColumnName("version");
        }
    }
}