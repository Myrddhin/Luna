using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Loki.Common;
using Luna.Data.Local.Storage;
using Luna.Data.Storage;
using Luna.Model;
using Luna.Model.Configuration;
using Luna.Model.Messages;
using Luna.Model.Storage;

namespace Luna.Services.Configuration
{
    internal class ApplicationConfigurationService : Service, IRepositoryManagerService, IHandle<RepositorySaveMessage>, IHandle<NetworkErrorMessage>
    {
        public IApplicationDataProvider DataProvider { get; set; }

        public IRepositoryDataContainer ClientDataContainer { get; set; }

        public IMessageComponent MessageBus { get; set; }

        public IEnumerable<RepositorySettings> LocalRepositories
        {
            get
            {
                return DataProvider.LocalRepositories;
            }
        }

        public Task<IEnumerable<Repository>> GetCloudRepositoriesAsync()
        {
            return DataProvider.GetCloudRepositoriesAsync();
        }

        public ApplicationConfigurationService()
        {
            install = new ContainerInstallService();
        }

        private ContainerInstallService install;

        public async Task SaveChangesAsync()
        {
            await DataProvider.SaveChangesAsync();
        }

        public RepositoryStatus? Status
        {
            get
            {
                if (ActiveRepository == null)
                {
                    return null;
                }
                else
                {
                    return install.GetStatus(GetDatabaseFileName(ActiveRepository));
                }
            }
        }

        #region Current

        public RepositorySettings DefaultRepository
        {
            get
            {
                return DataProvider.LocalRepositories.FirstOrDefault(x => x.Default);
            }
        }

        public RepositoryBase ActiveRepository
        {
            get;
            private set;
        }

        #endregion Current

        public async Task ConnectAsync(RepositoryBase repository)
        {
            if (!object.Equals(repository, ActiveRepository))
            {
                OnRepositoryPrepare();
                ActiveRepository = repository;

                if (Status == RepositoryStatus.OK)
                {
                    OnRepositoryReady();
                }
            }
        }

        private static string GetDatabaseFileName(RepositoryBase repository)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}.db3", repository.Id);
        }

        private RepositorySettings CreateRepository(RepositoryBase settings)
        {
            var newRepo = new RepositorySettings();
            newRepo.Id = ActiveRepository.Id;
            newRepo.Name = ActiveRepository.Name;
            newRepo.Online = ActiveRepository.Online;
            newRepo.LastUsed = DateTime.Now;
            return newRepo;
        }

        public async Task CreateAsync()
        {
            if (ActiveRepository != null)
            {
                var newRepo = CreateRepository(ActiveRepository);
                newRepo.SchemaVersion = RepositorySchemaVersion;

                await install.InstallAsync(GetDatabaseFileName(ActiveRepository));
                await DataProvider.SaveAsync(newRepo);

                // var paramInitializer = new ParameterAdapter(DataProvider);
                // paramInitializer.InitializeParameters(ParametersSets.Spectacle);

                await DataProvider.SaveChangesAsync();

                OnRepositoryReady();
            }
        }

        public async Task UpgradeAsync()
        {
            if (ActiveRepository != null)
            {
                await install.UpgradeAsync(GetDatabaseFileName(ActiveRepository));

                // upgrade parameters
                // var paramInitializer = new ParameterAdapter(DataProvider);
                // paramInitializer.UpgradeParameters();
                // await DataProvider.SaveChangesAsync();

                OnRepositoryReady();
            }
        }

        public bool IsReady()
        {
            return ActiveRepository != null && !string.IsNullOrEmpty(ActiveRepository.Name);
        }

        private void OnRepositoryPrepare()
        {
        }

        private void OnRepositoryReady()
        {
        }

        public string UserName
        {
            get { return Environment.UserName; }
        }

        public bool NetworkAvailable { get; set; }

        public override void BeginInit()
        {
            base.BeginInit();
            install.DataContainer = ClientDataContainer;
            MessageBus.Subscribe(this);
        }

        public void Handle(RepositorySaveMessage message)
        {
            var settings = DataProvider.LocalRepositories.FirstOrDefault(x => x.Id == message.Id);
            if (settings != null)
            {
                settings.Name = message.Name;
                settings.LastUsed = DateTime.Now;

                DataProvider.SaveAsync(settings).Wait();
                DataProvider.SaveChangesAsync().Wait();
            }
        }

        public void Handle(NetworkErrorMessage message)
        {
            this.NetworkAvailable = false;
            Log.Error(message.Error.Message, message.Error);
        }
    }
}