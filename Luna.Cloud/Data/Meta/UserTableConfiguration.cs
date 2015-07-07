using System.Data.Entity.ModelConfiguration;
using Luna.Model.Cloud.Meta;

namespace Luna.Cloud.Data.Meta
{
    public class UserTableConfiguration : EntityTypeConfiguration<User>
    {
        public UserTableConfiguration()
        {
            ToTable("meta.Users");
            HasKey(x => x.Id);

            Property(x => x.AzureLogin).HasColumnName("azure_login");
            Property(x => x.LastQuery).HasColumnName("last_query");
            Property(x => x.Id).HasColumnName("id_user");
            Property(x => x.SubscriptionId).HasColumnName("id_subscription");
        }
    }
}