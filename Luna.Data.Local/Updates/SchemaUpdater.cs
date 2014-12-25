using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Loki.Common;

namespace Luna.Data.Local.SQLite.Updates
{
    public abstract class SchemaUpdater
    {
        protected abstract Queue<string> GetUpgradeScripts();

        protected abstract Queue<string> GetDowngradeScripts();

        public void Upgrade(string connectionString)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                SQLiteTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    foreach (var commandText in GetUpgradeScripts())
                    {
                        using (var command = new SQLiteCommand(commandText, connection, transaction))
                        {
                            command.ExecuteNonQuery();
                        }
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
        }

        public void Downgrade(string connectionString)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                SQLiteTransaction transaction = null;
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    foreach (var commandText in GetDowngradeScripts())
                    {
                        using (var command = new SQLiteCommand(commandText, connection, transaction))
                        {
                            command.ExecuteNonQuery();
                        }
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
        }

        public abstract decimal Version { get; }
    }
}