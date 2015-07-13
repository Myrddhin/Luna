using System;
using System.IO;
using Luna.Data.Local.Storage;
using NewMoon = Luna.Data.Local.SQLite.Updates.NewMoon;

namespace Luna.Data.Local.SQLite
{
    internal class LocalRepositoryManager : DataContainer, IRepositoryDataContainer
    {
        public LocalRepositoryManager()
        {
            RegisterUpdater<NewMoon.LocalUpdater>();
            DefaultResourceName = "Luna.Data.Local.repository.db3";
        }

        public Guid InternalRepositoryId
        {
            get
            {
                return new Guid(Path.GetFileNameWithoutExtension(ConnectionBuilder.DataSource));
            }
        }
    }
}