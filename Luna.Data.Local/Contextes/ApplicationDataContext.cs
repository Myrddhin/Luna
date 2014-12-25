using System.Data.Entity;
using Luna.Data.Local.SQLite.TablesConfiguration;
using Luna.Data.Local.Storage;
using Luna.Model.Storage;

namespace Luna.Data.Local.SQLite
{
    public class ApplicationDataContext : SQLiteContext<ApplicationDataContext>, IApplicationDataContext
    {
        public ApplicationDataContext(string connectionString)
            : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new RepositoryConfiguration());
        }

        public virtual IDbSet<Repository> Repositories { get; set; }
    }
}