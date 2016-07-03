using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Luna.Model;

namespace Luna.Data.Cloud
{
    public class AzureRequester : Service, IDisposable
    {
        public string ADUrl { get; set; }

        public string ApiUri { get; set; }

        public string LunaID { get; set; }

        public string ApiRoot { get; set; }

        public string LunaReturnUrl { get; set; }

        private HttpClient client;

        private FileCache TokenCache { get; set; }

        public AzureRequester()
        {
            ADUrl = "https://login.windows.net/lunacloudapps.fr";
            ApiUri = "https://lunacloudapps/LunaWebApi";
            LunaID = "fd185a5f-9d4e-4681-be60-009cfeedb958";
            LunaReturnUrl = "https://lunacloudapps.fr/luna";
            //ApiRoot = "http://luna-web.azurewebsites.net/api/";
            ApiRoot = "https://localhost:44301/api/";

            TokenCache = new FileCache(Path.Combine(DataFolder, "AzureTokenCache.dat"));

            client = new HttpClient { Timeout = new TimeSpan(0, 0, 10) };
        }

        private void CheckAuth()
        {
            //AuthenticationContext ac = new AuthenticationContext(ADUrl, TokenCache);
            //AuthenticationResult ar = ac.AcquireToken(ApiUri, LunaID, new Uri(LunaReturnUrl));

            // Call Web API
            //string authHeader = ar.CreateAuthorizationHeader();

            //client.DefaultRequestHeaders.Authorization = authHeader;
        }

        public bool CheckNet()
        {
            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();

            System.Net.NetworkInformation.PingReply pingStatus = ping.Send("www.google.com", 200);

            if (pingStatus.Status == System.Net.NetworkInformation.IPStatus.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<HttpResponseMessage> GetAsync(Uri uri)
        {
            CheckAuth();
            return await client.GetAsync(uri);
        }

        public async Task<HttpResponseMessage> PostAsync<T>(Uri uri, T content)
        {
            CheckAuth();
            return await client.PutAsJsonAsync(uri, content);
        }

        public async Task<HttpResponseMessage> PutAsync<T>(Uri uri, T content)
        {
            CheckAuth();
            return await client.PutAsJsonAsync(uri, content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(Uri uri)
        {
            CheckAuth();
            return await client.DeleteAsync(uri);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            client.Dispose();
        }
    }
}