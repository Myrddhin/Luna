using Luna.Data.Local.Storage;
using NewMoon = Luna.Data.Local.SQLite.Updates.NewMoon;

namespace Luna.Data.Local.SQLite
{
    public class GlobalRepositoryManager : RepositoryManager, IGlobalRepositoryManager
    {
        public GlobalRepositoryManager()
        {
            RegisterUpdater<NewMoon.GlobalUpdater>();
            DefaultResourceName = "Luna.Data.Local.applicationdata.db3";
        }

        public IApplicationDataContext GetApplicationDataContext()
        {
            return new ApplicationDataContext(ConnectionString);
        }
    }
}