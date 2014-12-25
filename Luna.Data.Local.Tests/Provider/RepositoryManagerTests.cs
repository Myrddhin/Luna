using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Loki.Common;
using Xunit;

namespace Luna.Data.Local.SQLite.Tests
{
    public class RepositoryManagerTests : IDisposable
    {
        public RepositoryManagerTests()
        {
            Toolkit.Initialize();
            manager = new GlobalRepositoryManager();
        }

        private GlobalRepositoryManager manager;

        [Fact]
        public void CreateTest()
        {
            manager.Create("test.db3");
            Assert.True(File.Exists("test.db3"));

            ValidateConnection();
            Assert.True(manager.SchemaVersion == 0);
            manager.UpgradeToVersion(1);

            Assert.True(manager.SchemaVersion == 1);

            var context = manager.GetApplicationDataContext();

            Assert.True(context.Repositories.Count() == 0);
        }

        private void ValidateConnection()
        {
            using (var connection = new SQLiteConnection(manager.ConnectionString))
            using (var command = new SQLiteCommand("SELECT MAX(Version) FROM SchemaVersions", connection))
            {
                connection.Open();

                var result = command.ExecuteScalar(System.Data.CommandBehavior.CloseConnection);

                connection.Close();
            }
        }

        [Fact]
        public void OpenTest()
        {
            Assert.True(false, "not implemented yet");
        }

        [Fact]
        public void SaveAsTest()
        {
            Assert.True(false, "not implemented yet");
        }

        [Fact]
        public void DisconnectTest()
        {
            Assert.True(false, "not implemented yet");
        }

        public void Dispose()
        {
            manager.Disconnect();
            Toolkit.Reset();
            //File.Delete("test.db3");
        }
    }
}