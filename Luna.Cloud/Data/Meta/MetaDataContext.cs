using System.Data.Entity;
using Luna.Model.Cloud.Meta;

namespace Luna.Cloud.Data.Meta
{
    public class MetaDataContext : LunaDataContext
    {
        public virtual DbSet<Repository> Repositories { get; set; }

        public virtual DbSet<Subscription> Subscriptions { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new RepositoryTableConfiguration());
            modelBuilder.Configurations.Add(new SubscriptionTableConfiguration());
            modelBuilder.Configurations.Add(new UserTableConfiguration());
        }
    }
}