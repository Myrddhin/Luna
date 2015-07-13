using System.Data.Entity;
using Luna.Data.Local.CRM;
using Luna.Data.Local.SQLite.TablesConfiguration;
using Luna.Data.Local.Storage;
using Luna.Model.CRM;

namespace Luna.Data.Local.SQLite
{
    internal class CRMContext : SQLiteContext<CRMContext>, ICRMDataContext
    {
        public CRMContext(IRepositoryDataContainer dataContainer)
            : base(dataContainer.ConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AddressConfiguration());
            modelBuilder.Configurations.Add(new TagConfiguration());
            modelBuilder.Configurations.Add(new ContactConfiguration());
        }

        public virtual DbSet<Address> Addresses { get; set; }

        public virtual DbSet<Contact> Contacts { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }
    }
}