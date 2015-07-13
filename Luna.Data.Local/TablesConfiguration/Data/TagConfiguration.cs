using System.Data.Entity.ModelConfiguration;
using Luna.Model.CRM;

namespace Luna.Data.Local.SQLite.TablesConfiguration
{
    internal class TagConfiguration : EntityTypeConfiguration<Tag>
    {
        public TagConfiguration()
        {
            ToTable("Tags");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("id_tag");
            Property(x => x.Name).HasColumnName("name");
            Property(x => x.Color).HasColumnName("color");
            Property(x => x.RepositoryId).HasColumnName("id_repository");
            Property(x => x.LastUpdate).HasColumnName("last_update");
            Property(x => x.Version).HasColumnName("version");
            HasMany(x => x.Contacts).WithMany(x => x.Tags).Map(m =>
            {
                m.ToTable("R_Contacts_Tags");
                m.MapLeftKey("id_tag");
                m.MapRightKey("id_contact");
            });
        }
    }
}