using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite;
using System.IO;
using System.Reflection;

namespace Luna.Data.Local.SQLite
{
    public class SQLiteConnectionFactory : IDbConnectionFactory
    {
        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            return Create(nameOrConnectionString, true);
        }

        private static readonly object Lock = new object();
        private static bool _haveSetPath;

        /// <summary>
        /// Loads mod_spatialite.dll on the given connection
        /// </summary>
        public static void LoadExtensions(SQLiteConnection conn)
        {
            lock (Lock)
            {
                // Need to work out where the file is and add it to the path so it can load all the other dlls too
                if (!_haveSetPath)
                {
                    var spatialitePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase), (Environment.Is64BitProcess ? "x64" : "x86"), "spatialite");
                    spatialitePath = spatialitePath.Replace("file:\\", "");
                    Environment.SetEnvironmentVariable("PATH", spatialitePath + ";" + Environment.GetEnvironmentVariable("PATH"));
                    _haveSetPath = true;
                }
            }

            conn.Open();
            conn.EnableExtensions(true);
            conn.LoadExtension("mod_spatialite");
        }

        public static SQLiteConnection Create(string nameOrConnectionString, bool opened)
        {
            var connection = new SQLiteConnection(nameOrConnectionString);

            LoadExtensions(connection);

            return connection;
        }
    }
}