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

        protected async Task<T> ExecuteRequestAsync<T>(HttpRequestMessage message, T defaultValue)
        {
            try
            {
                var response = await Requester.Send(message);
                return await response.Content.ReadAsAsync<T>();
            }
            catch (Exception ex)
            {
                MessageBus.PublishOnCurrentThread(new NetworkErrorMessage(ex));
                return defaultValue;
            }
        }

        protected async Task<IEnumerable<TClient>> GetListFromCloud(HttpRequestMessage message)
        {
            var result = await ExecuteRequestAsync<IEnumerable<TCloud>>(message, new TCloud[] { });

            return result.ToList().ConvertAll(converter.FromCloud);
        }

        protected async Task<TClient> GetItemFromCloud(HttpRequestMessage message)
        {
            var result = await ExecuteRequestAsync<TCloud>(message, default(TCloud));

            return result != null ? converter.FromCloud(result) : default(TClient);
        }
    }
}