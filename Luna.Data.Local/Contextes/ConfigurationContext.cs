using System.Data.Entity;
using Luna.Data.Local.Configuration;
using Luna.Data.Local.SQLite.TablesConfiguration;
using Luna.Model.Configuration;

namespace Luna.Data.Local.SQLite
{
    internal class ConfigurationContext : SQLiteContext<ConfigurationContext>, IConfigurationContext
    {
        public ConfigurationContext(string connectionString)
            : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new ProviderConfigurationConfiguration());
        }

        public virtual IDbSet<ProviderConfiguration> Providers { get; set; }
    }
}