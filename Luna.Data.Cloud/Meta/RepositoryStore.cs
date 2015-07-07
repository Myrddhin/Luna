using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Luna.Model.Storage;
using Luna.Services.Configuration;
using Remote = Luna.Model.Cloud.Meta;

namespace Luna.Data.Cloud.Azure
{
    public class RepositoryStore : BaseStore<Repository, Remote.Repository>, IRepositoryStore
    {
        public RepositoryStore()
        {
            converter = new RepositoryConverter();
        }

        public async Task<IEnumerable<Repository>> GetAllAsync()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, Requester.ApiRoot + "/repositories");

            return await this.GetListFromCloud(message);
        }

        public async Task<Repository> GetAsync(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Requester.ApiRoot + "/repositories/" + id.ToString());
            return await this.GetItemFromCloud(request);
        }

        public async Task<IEnumerable<Repository>> GetAllAsync(DateTime refreshDate)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                Requester.ApiRoot + "/repositories/date=?" +
                WebUtility.UrlEncode(refreshDate.ToUniversalTime().ToString("o")));
            return await this.GetListFromCloud(request);
        }

        public Task SaveAsync(Repository item)
        {
            throw new NotImplementedException();
        }
    }
}