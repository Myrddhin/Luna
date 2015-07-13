using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
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
            string apiRoot = string.Format(CultureInfo.InvariantCulture, "{0}/repository/{1}/tags/lastmodified", Requester.ApiRoot, repositoryId);
            var message = new HttpRequestMessage(HttpMethod.Get, apiRoot);

            return await this.GetScalarFromCloud<DateTime>(message);
        }

        public async Task<IEnumerable<Tag>> GetAllAsync(Guid repositoryId)
        {
            return await GetAllAsync(repositoryId, DateTime.MinValue);
        }

        public async Task<Tag> GetAsync(Guid repositoryId, Guid id)
        {
            string apiRoot = string.Format(CultureInfo.InvariantCulture, "{0}/repository/{1}/tags", Requester.ApiRoot, repositoryId);
            var request = new HttpRequestMessage(HttpMethod.Get, apiRoot + "/" + id.ToString());
            return await this.GetItemFromCloud(request);
        }

        public async Task<IEnumerable<Tag>> GetAllAsync(Guid repositoryId, DateTime refreshDate)
        {
            string apiRoot = string.Format(
                CultureInfo.InvariantCulture,
                "{0}/repository/{1}/tags",
                Requester.ApiRoot,
                repositoryId);
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                apiRoot + "/?date=" + WebUtility.UrlEncode(refreshDate.ToUniversalTime().ToString("o")));
            return await this.GetListFromCloud(request);
        }

        public Task SaveAsync(Tag item)
        {
            throw new NotImplementedException();
        }
    }
}