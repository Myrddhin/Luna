using System.Data.Entity;
using Luna.Model.CRM;

namespace Luna.Data.Local.CRM
{
    public interface ICRMDataContext : IDataContext
    {
        DbSet<Address> Addresses { get; }

        DbSet<Tag> Tags { get; set; }

        DbSet<Contact> Contacts { get; set; }
    }
}