using System.Data.Entity;
using Luna.Data.Local.Configuration;
using Luna.Data.Local.SQLite.TablesConfiguration;
using Luna.Data.Local.Storage;
using Luna.Model.Storage;

namespace Luna.Data.Local.SQLite
{
    internal class ConfigurationContext : SQLiteContext<ConfigurationContext>, IConfigurationContext
    {
        public ConfigurationContext(IRepositoryDataContainer dataContainer)
            : base(dataContainer.ConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new RepositoryConfiguration());
        }

        public virtual DbSet<Repository> Repositories { get; set; }
    }
}