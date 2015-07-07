using System.Data.Entity.ModelConfiguration;
using Luna.Model.Cloud.Meta;

namespace Luna.Cloud.Data
{
    public class SubscriptionTableConfiguration : EntityTypeConfiguration<Subscription>
    {
        public SubscriptionTableConfiguration()
        {
            ToTable("meta.Subscriptions");
            HasKey(x => x.Id);

            Property(x => x.Closing).HasColumnName("closing");
            Property(x => x.Name).HasColumnName("name");
            Property(x => x.Start).HasColumnName("start");
            Property(x => x.Id).HasColumnName("id_subscription");
        }
    }
}