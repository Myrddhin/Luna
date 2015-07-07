using Luna.Data.Local.Storage;
using NewMoon = Luna.Data.Local.SQLite.Updates.NewMoon;

namespace Luna.Data.Local.SQLite
{
    internal class GlobalRepositoryManager : DataContainer, IApplicationDataContainer
    {
        public GlobalRepositoryManager()
        {
            RegisterUpdater<NewMoon.GlobalUpdater>();
            DefaultResourceName = "Luna.Data.Local.applicationdata.db3";
        }
    }
}