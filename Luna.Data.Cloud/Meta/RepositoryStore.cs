using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Luna.Model.Storage;
using Luna.Services.Configuration;
using Remote = Luna.Model.Cloud.Meta;

namespace Luna.Data.Cloud.Meta
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

            return await this.GetListFromCloud("/repositories");
        }

        public async Task<Repository> GetAsync(Guid id)
        {
            return await this.GetItemFromCloud("/repositories/" + id.ToString());
        }

        public async Task<IEnumerable<Repository>> GetAllAsync(DateTime refreshDate)
        {
            return await this.GetListFromCloud("/repositories/date=?" +
                WebUtility.UrlEncode(refreshDate.ToUniversalTime().ToString("o")));
        }

        public Task SaveAsync(Repository item)
        {
            throw new NotImplementedException();
        }
    }
}