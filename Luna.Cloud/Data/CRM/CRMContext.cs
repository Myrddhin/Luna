using System.Data.Entity;
using Luna.Model.Cloud.CRM;

namespace Luna.Cloud.Data.CRM
{
    public class CRMContext : LunaDataContext
    {
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TagTableConfiguration());
        }
    }
}