using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Luna.Data.Local.CRM;
using Luna.Data.Storage;
using Luna.Model;
using Luna.Model.CRM;

namespace Luna.Data.CRM
{
    internal class CRMDataProvider : BaseProvider, ICRMDataProvider
    {
        public ICRMDataContext DataContext { get; set; }

        public ICloudStore<Tag> TagStore { get; set; }

        public async Task EnsureCloudRefresh()
        {
            if (State.ActiveRepository.Online)
            {
                IEnumerable<Tag> modifiedOnline = new List<Tag>();
                IEnumerable<Tag> modifiedLocal = new List<Tag>();

                if (!DataContext.Tags.Any())
                {
                    modifiedOnline = await TagStore.GetAllAsync(State.ActiveRepository.Id);
                }
                else
                {
                    DateTime maxLocal = await DataContext.Tags.MaxAsync(x => x.LastUpdate);
                    DateTime maxCloud = await TagStore.LastModified(State.ActiveRepository.Id);

                    modifiedOnline = await TagStore.GetAllAsync(State.ActiveRepository.Id, maxLocal);
                    modifiedLocal = DataContext.Tags.Where(x => x.LastUpdate > maxCloud);
                }

                foreach (var tag in modifiedLocal)
                {
                    await TagStore.SaveAsync(tag);
                }

                foreach (var item in modifiedOnline)
                {
                    await SaveAsync(item);
                }

                await this.SaveChangesAsync();
            }
            else
            {
                await Task.FromResult(false);
            }
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