using System;
using System.IO;
using System.Threading.Tasks;
using Luna.Data.Storage;
using Luna.Model.Storage;
using Luna.Services.Common;

namespace Luna.Services.Configuration
{
    internal class ContainerInstallService : BaseService
    {
        public IDataContainer DataContainer { get; set; }

        private const decimal SchemaVersion = 1;

        protected string DataFolder
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData, Environment.SpecialFolderOption.Create), "Luna");
            }
        }

        private string GetApplicationFilePath(string applicationFile)
        {
            return Path.Combine(DataFolder, applicationFile);
        }

        public async Task UpgradeAsync(string containerFile)
        {
            string applicationFilePath = GetApplicationFilePath(containerFile);

            await Task.Factory.StartNew(() =>
            {
                DataContainer.Disconnect();
                DataContainer.Open(applicationFilePath);
                DataContainer.UpgradeToVersion(SchemaVersion);
            });
        }

        public async Task InstallAsync(string containerFile)
        {
            string applicationFilePath = GetApplicationFilePath(containerFile);

            await Task.Factory.StartNew(() =>
            {
                if (!Directory.Exists(DataFolder))
                {
                    Directory.CreateDirectory(DataFolder);
                }

                DataContainer.Disconnect();
                DataContainer.Create(applicationFilePath);
                DataContainer.Open(applicationFilePath);

                DataContainer.UpgradeToVersion(SchemaVersion);
            });
        }

        public RepositoryStatus GetStatus(string containerFile)
        {
            string applicationFilePath = GetApplicationFilePath(containerFile);

            if (!File.Exists(applicationFilePath))
            {
                return RepositoryStatus.New;
            }
            else
            {
                if (DataContainer.ConnectionString == null)
                {
                    DataContainer.Open(applicationFilePath);
                }

                if (DataContainer.SchemaVersion < SchemaVersion)
                {
                    return RepositoryStatus.NeedUpgrade;
                }
                else
                {
                    return RepositoryStatus.OK;
                }
            }
        }
    }
}