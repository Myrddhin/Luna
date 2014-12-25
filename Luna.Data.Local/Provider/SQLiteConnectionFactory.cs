using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite;

namespace Luna.Data.Local.SQLite
{
    public class SQLiteConnectionFactory : IDbConnectionFactory
    {
        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            return new SQLiteConnection(nameOrConnectionString);
        }
    }
}