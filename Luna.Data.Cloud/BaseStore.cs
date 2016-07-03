using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Loki.Common;
using Luna.Model.Messages;

namespace Luna.Data.Cloud
{
    public class BaseStore<TClient, TCloud>
    {
        protected CloudConverter<TClient, TCloud> converter;

        public AzureRequester Requester { get; set; }

        public IMessageComponent MessageBus { get; set; }

        private async Task<T> ExecuteGetAsync<T>(string address, T defaultValue)
        {
            try
            {
                var uri = new Uri(Requester.ApiRoot + address);
                var response = await Requester.GetAsync(uri);
                return await response.Content.ReadAsAsync<T>();
            }
            catch (Exception ex)
            {
                MessageBus.PublishOnCurrentThread(new NetworkErrorMessage(ex));
                return defaultValue;
            }
        }

        private async Task ExecutePutAsync<T>(string address, T value)
        {
            try
            {
                var uri = new Uri(Requester.ApiRoot + address);
                await Requester.PutAsync(uri, value);
            }
            catch (Exception ex)
            {
                MessageBus.PublishOnCurrentThread(new NetworkErrorMessage(ex));
            }
        }

        private async Task ExecuteDeleteAsync(string address)
        {
            try
            {
                var uri = new Uri(Requester.ApiRoot + address);
                await Requester.DeleteAsync(uri);
            }
            catch (Exception ex)
            {
                MessageBus.PublishOnCurrentThread(new NetworkErrorMessage(ex));
            }
        }

        protected async Task<IEnumerable<TClient>> GetListFromCloud(string address)
        {
            var result = await ExecuteGetAsync<IEnumerable<TCloud>>(address, new TCloud[] { });

            return result.ToList().ConvertAll(converter.FromCloud);
        }

        protected async Task<TClient> GetItemFromCloud(string address)
        {
            var result = await ExecuteGetAsync(address, default(TCloud));

            return result != null ? converter.FromCloud(result) : default(TClient);
        }

        protected async Task<TScalar> GetScalarFromCloud<TScalar>(string address)
        {
            return await ExecuteGetAsync(address, default(TScalar));
        }

        protected async Task SaveItemToCloud(string address, TClient item)
        {
            var cloud = converter.ToCloud(item);
            await ExecutePutAsync(address, cloud);
        }

        protected async Task DeleteItemToCloud(string address)
        {
            await ExecuteDeleteAsync(address);
        }
    }
}