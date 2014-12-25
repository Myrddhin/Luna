using Luna.Data.Local.Configuration;
using Luna.Data.Local.Storage;
using NewMoon = Luna.Data.Local.SQLite.Updates.NewMoon;

namespace Luna.Data.Local.SQLite
{
    public class LocalRepositoryManager : RepositoryManager, ILocalRepositoryManager
    {
        public LocalRepositoryManager()
        {
            RegisterUpdater<NewMoon.LocalUpdater>();
            DefaultResourceName = "Luna.Data.Local.repository.db3";
        }

        public IConfigurationContext GetConfigurationContext()
        {
            return new ConfigurationContext(ConnectionString);
        }
    }
}