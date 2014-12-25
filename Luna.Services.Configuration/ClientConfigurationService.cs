using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using Loki.Common;
using Luna.Data.Storage;
using Luna.Model.Storage;

namespace Luna.Services.Configuration
{
    internal class ClientConfigurationService : BaseConfigurationService, IRepositoryConfiguration
    {
        public IApplicationDataProvider DataProvider { get; set; }

        public RepositoryStatus? Status
        {
            get
            {
                if (Current == null)
                {
                    return null;
                }
                else
                {
                    return GetStatus(Current);
                }
            }
        }

        #region Current

        private PropertyChangedEventArgs currentChangedEventArgs = ObservableHelper.CreateChangedArgs<ClientConfigurationService>(x => x.Current);

        private Repository current;

        public Repository Current
        {
            get
            {
                return current;
            }

            set
            {
                if (!Object.Equals(current, value))
                {
                    current = value;
                    NotifyChanged(currentChangedEventArgs);
                }
            }
        }

        #endregion Current

        private RepositoryStatus GetStatus(Repository repository)
        {
            if (!File.Exists(Path.Combine(DataFolder, GetDatabaseFileName(repository))))
            {
                return RepositoryStatus.New;
            }
            else
            {
                if (DataProvider.ClientStore.ConnectionString == null)
                {
                    DataProvider.ClientStore.Open(Path.Combine(DataFolder, GetDatabaseFileName(repository)));
                }

                if (DataProvider.ClientStore.SchemaVersion < RepositorySchemaVersion)
                {
                    return RepositoryStatus.NeedUpgrade;
                }
                else
                {
                    return RepositoryStatus.OK;
                }
            }
        }

        private static string GetDatabaseFileName(Repository repository)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}.db3", repository.Id);
        }

        public void Create()
        {
            if (Current != null)
            {
                Current.SchemaVersion = RepositorySchemaVersion;
                DataProvider.ClientStore.Disconnect();
                DataProvider.ClientStore.Create(Path.Combine(DataFolder, GetDatabaseFileName(Current)));
                DataProvider.ClientStore.Open(Path.Combine(DataFolder, GetDatabaseFileName(Current)));
                DataProvider.ClientStore.UpgradeToVersion(Current.SchemaVersion);
                DataProvider.SaveRepository(Current);
            }
        }

        public void Upgrade()
        {
            throw new NotImplementedException();
        }

        public bool IsReady()
        {
            return Current != null && !string.IsNullOrEmpty(Current.Name);
        }
    }
}