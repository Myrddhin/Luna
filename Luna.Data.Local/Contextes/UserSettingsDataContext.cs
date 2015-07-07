using System.Data.Entity;
using Luna.Data.Local.SQLite.TablesConfiguration;
using Luna.Data.Local.Storage;
using Luna.Model.Storage;

namespace Luna.Data.Local.SQLite
{
    public class UserSettingsDataContext : SQLiteContext<UserSettingsDataContext>, IApplicationSettingsDataContext
    {
        public UserSettingsDataContext(IApplicationDataContainer dataContainer)
            : base(dataContainer.ConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new RepositorySettingsConfiguration());
        }

        public virtual DbSet<RepositorySettings> RepositorySettings { get; set; }
    }
}