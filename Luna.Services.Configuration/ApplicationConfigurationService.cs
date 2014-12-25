using System.Collections.Generic;
using System.IO;
using Luna.Data.Storage;
using Luna.Model.Storage;

namespace Luna.Services.Configuration
{
    internal class ApplicationConfigurationService : BaseConfigurationService, IApplicationConfiguration
    {
        public IApplicationDataProvider DataProvider { get; set; }

        public IRepositoryConfiguration ClientConfiguration { get; set; }

        public RepositoryStatus ApplicationStatus
        {
            get { return GetStatus(); }
        }

        private const string ApplicationFileName = "applicationdata.db3";

        private string ApplicationFile
        {
            get
            {
                return Path.Combine(DataFolder, ApplicationFileName);
            }
        }

        private RepositoryStatus GetStatus()
        {
            if (!File.Exists(ApplicationFile))
            {
                return RepositoryStatus.New;
            }
            else
            {
                if (DataProvider.ApplicationStore.ConnectionString == null)
                {
                    DataProvider.ApplicationStore.Open(ApplicationFile);
                }

                if (DataProvider.ApplicationStore.SchemaVersion < GlobalSchemaVersion)
                {
                    return RepositoryStatus.NeedUpgrade;
                }
                else
                {
                    return RepositoryStatus.OK;
                }
            }
        }

        public void Install()
        {
            if (!Directory.Exists(DataFolder))
            {
                Directory.CreateDirectory(DataFolder);
            }

            DataProvider.ApplicationStore.Disconnect();
            DataProvider.ApplicationStore.Create(ApplicationFile);
            DataProvider.ApplicationStore.Open(ApplicationFile);

            DataProvider.ApplicationStore.UpgradeToVersion(GlobalSchemaVersion);
        }

        public void Upgrade()
        {
            if (DataProvider.ApplicationStore.ConnectionString == null)
            {
                DataProvider.ApplicationStore.Open(ApplicationFile);
            }

            DataProvider.ApplicationStore.UpgradeToVersion(GlobalSchemaVersion);
        }

        public IEnumerable<Repository> Repositories
        {
            get
            {
                return DataProvider.Repositories;
            }
        }
    }
}