using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Luna.Data.Local.CRM;
using Luna.Data.Storage;
using Luna.Model.CRM;

namespace Luna.Data.CRM
{
    internal class CRMDataProvider : BaseProvider, ICRMDataProvider
    {
        protected ICRMDataContext DataContext { get; set; }

        protected override void GetClientContextes()
        {
            DataContext = this.ClientDataContainer.GetCRMContext();
        }

        protected override void CleanClientContextes()
        {
            DataContext.SafeDispose();
        }

        public IQueryable<Contact> Contacts
        {
            get { return DataContext.Contacts.Include(x => x.Address).Include(x => x.Tags); }
        }

        public IQueryable<Tag> Tags
        {
            get { return DataContext.Tags; }
        }

        public async Task SaveAsync(Contact contact)
        {
            await Save<Contact>(DataContext, DataContext.Contacts, contact);
        }

        public async Task SaveAsync(Tag tag)
        {
            await Save<Tag>(DataContext, DataContext.Tags, tag);
        }

        public async Task RemoveAsync(Contact contact)
        {
            await Remove<Contact>(DataContext, DataContext.Contacts, contact);
        }

        public async Task RemoveAsync(Tag tag)
        {
            await Remove<Tag>(DataContext, DataContext.Tags, tag);
        }

        public async Task SaveChangesAsync()
        {
            await DataContext.SaveChangesAsync();
        }
    }
}