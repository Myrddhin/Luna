using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

using Luna.Model;
using Luna.Model.CRM;
using Remote = Luna.Model.Cloud.CRM;

namespace Luna.Data.Cloud.CRM
{
    public class TagStore : BaseStore<Tag, Remote.Tag>, ICloudStore<Tag>
    {
        public TagStore()
        {
            converter = new TagConverter();
        }

        public async Task<DateTime> LastModified(Guid repositoryId)
        {
            string address = string.Format(CultureInfo.InvariantCulture, "/repository/{0}/tags/lastmodified", repositoryId);

            return await GetScalarFromCloud<DateTime>(address);
        }

        public async Task<IEnumerable<Tag>> GetAllAsync(Guid repositoryId)
        {
            return await GetAllAsync(repositoryId, DateTime.MinValue);
        }

        public async Task<Tag> GetAsync(Guid repositoryId, Guid id)
        {
            string address = string.Format(CultureInfo.InvariantCulture, "/repository/{0}/tags/{1}", repositoryId, id);
            return await GetItemFromCloud(address);
        }

        public async Task<IEnumerable<Tag>> GetAllAsync(Guid repositoryId, DateTime refreshDate)
        {
            string address = string.Format(
                CultureInfo.InvariantCulture,
                "/repository/{0}/tags/?date={1}",
                repositoryId,
                WebUtility.UrlEncode(refreshDate.ToUniversalTime().ToString("o")));
            return await GetListFromCloud(address);
        }

        public async Task SaveAsync(Guid repositoryId, Tag item)
        {
            string address = string.Format(
                  CultureInfo.InvariantCulture,
                  "/repository/{0}/tags",
                  repositoryId);
            await SaveItemToCloud(address, item);
        }

        public async Task DeleteAsync(Guid repositoryId, Guid id)
        {
            string address = string.Format(
                  CultureInfo.InvariantCulture,
                  "/repository/{0}/tags/{1}",
                  repositoryId,
                  id);

            await DeleteItemToCloud(address);
        }
    }
}