using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using Loki.Common;
using Luna.Data.Local.Resources;
using Luna.Data.Local.SQLite.Updates;
using Luna.Data.Storage;

namespace Luna.Data.Local.SQLite
{
    public class RepositoryManager : BaseObject, IRepositoryManager
    {
        public string DefaultResourceName { get; set; }

        public void Create(string databaseName)
        {
            FileStream stream = null;
            try
            {
                stream = File.Create(databaseName);
                Stream file = Assembly.GetExecutingAssembly().GetManifestResourceStream(DefaultResourceName);
                file.CopyTo(stream);
                stream.Close();
                file.Close();
            }
            finally
            {
                if (stream != null)
                {
                    stream.Dispose();
                }
            }

            InitializeConnection(databaseName);
        }

        public void Open(string databaseName)
        {
            if (File.Exists(databaseName))
            {
                try
                {
                    InitializeConnection(databaseName);
                }
                catch (Exception ex)
                {
                    throw BuildErrorFormat<LokiException>(Errors.SQLiteDatabaseManager_ConnectionError, databaseName, ex);
                }
            }
            else
            {
                throw BuildErrorFormat<LokiException>(Errors.SQLiteDatabaseManager_FileNotFound, databaseName);
            }
        }

        public void SaveAs(string databaseName)
        {
            if (!string.IsNullOrEmpty(ConnectionBuilder.DataSource))
            {
                try
                {
                    if (File.Exists(databaseName))
                    {
                        File.Delete(databaseName);
                    }

                    File.Copy(ConnectionBuilder.DataSource, databaseName);
                    InitializeConnection(databaseName);
                }
                catch (Exception ex)
                {
                    throw BuildErrorFormat<LokiException>(Errors.SQLiteDatabaseManager_ConnectionError, ex, databaseName);
                }
            }
            else
            {
                throw BuildError<LokiException>(Errors.SQLiteDatabaseManager_NoCurrentDatabase);
            }
        }

        private SQLiteConnectionStringBuilder connectionBuilder = new SQLiteConnectionStringBuilder();

        protected SQLiteConnectionStringBuilder ConnectionBuilder
        {
            get
            {
                return connectionBuilder;
            }
        }

        public decimal SchemaVersion
        {
            get
            {
                return GetSchemaVersion();
            }
        }

        public string ConnectionString
        {
            get
            {
                if (!string.IsNullOrEmpty(connectionBuilder.DataSource))
                {
                    return connectionBuilder.ConnectionString;
                }
                else
                {
                    return null;
                }
            }
        }

        private void InitializeConnection(string databaseName)
        {
            connectionBuilder.DataSource = databaseName;

            OnConnectionChanged(EventArgs.Empty);

            Log.InfoFormat(LogStrings.SQLiteDatabaseManager_Connect, connectionBuilder.ToString());
        }

        public void Disconnect()
        {
            connectionBuilder.DataSource = null;

            OnConnectionChanged(EventArgs.Empty);
        }

        private decimal GetSchemaVersion()
        {
            object buffer = null;

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                SQLiteTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    using (var command = new SQLiteCommand("SELECT MAX(Version) FROM SchemaVersions", connection, transaction))
                    {
                        buffer = command.ExecuteScalar();
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }

                    throw;
                }
                finally
                {
                    transaction.SafeDispose();
                }
            }

            if (buffer != null && buffer != DBNull.Value)
            {
                return Convert.ToDecimal(buffer);
            }
            else
            {
                return 0;
            }
        }

        private SortedList<decimal, SchemaUpdater> updaters = new SortedList<decimal, SchemaUpdater>();

        public void RegisterUpdater<T>() where T : SchemaUpdater, new()
        {
            T updater = new T();
            updaters.Add(updater.Version, updater);
        }

        public void UpgradeToVersion(decimal newVersion)
        {
            foreach (var updater in updaters)
            {
                if (updater.Key <= SchemaVersion)
                {
                    continue;
                }

                if (updater.Key > SchemaVersion && newVersion <= updater.Key)
                {
                    updater.Value.Upgrade(ConnectionString);
                }
            }
        }

        #region ConnectionChanged

        public event EventHandler ConnectionChanged;

        protected virtual void OnConnectionChanged(EventArgs e)
        {
            EventHandler handler = ConnectionChanged;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion ConnectionChanged
    }
}