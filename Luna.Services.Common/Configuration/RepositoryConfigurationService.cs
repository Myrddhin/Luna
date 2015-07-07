using System;
using System.Linq;
using System.Threading.Tasks;
using Loki.Common;
using Luna.Data.Storage;
using Luna.Model.Configuration;
using Luna.Model.Storage;
using Luna.Services.Common;

namespace Luna.Services.Configuration
{
    internal class RepositoryConfigurationService : BaseService, IRepositoryConfigurationService
    {
        public IRepositoryManagerService InstallService { get; set; }

        public IConfigurationDataProvider DataProvider { get; set; }

        public async Task SaveChangesAsync()
        {
            await DataProvider.SaveAsync(Current);
            await DataProvider.SaveChangesAsync();

            MessageBus.PublishOnUIThread(new RepositorySaveMessage() { Id = Current.Id, Name = Current.Name });
        }

        protected Repository Current
        {
            get
            {
                if (!DataProvider.Repositories.Any())
                {
                    // New offline repository
                    var baseInfos = InstallService.ActiveRepository;
                    var current = new Repository() { Id = baseInfos.Id, LastUpdate = DateTime.Now, Name = baseInfos.Name, Online = false, SchemaVersion = baseInfos.SchemaVersion };
                    DataProvider.SaveAsync(current).Wait();
                    DataProvider.SaveChangesAsync().Wait();
                }

                return DataProvider.Repositories.FirstOrDefault();
            }
        }

        public string Name
        {
            get { return Current.Name; }
            set { Current.Name = value; }
        }

        public bool IsOnline
        {
            get { return Current.Online; }
            set { Current.Online = value; }
        }
    }
}