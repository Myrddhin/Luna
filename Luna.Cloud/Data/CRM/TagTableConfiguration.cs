using System.Data.Entity.ModelConfiguration;
using Luna.Model.Cloud.CRM;

namespace Luna.Cloud.Data.CRM
{
    public class TagTableConfiguration : EntityTypeConfiguration<Tag>
    {
        public TagTableConfiguration()
        {
            ToTable("crm.Tags");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("id_tag");
            Property(x => x.LastUpdate).HasColumnName("last_update");
            Property(x => x.Name).HasColumnName("name");
            Property(x => x.RepositoryId).HasColumnName("id_repository");
            Property(x => x.Color).HasColumnName("color");
            Property(x => x.Version).HasColumnName("version");
        }
    }
}