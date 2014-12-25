using System;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.Reflection;

namespace Luna.Data.Local.SQLite
{
    public class SQLiteConfiguration : DbConfiguration
    {
        public SQLiteConfiguration()
        {
            SetDefaultConnectionFactory(new SQLiteConnectionFactory());
            SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance);
            SetProviderFactory("System.Data.SQLite.EF6", SQLiteProviderFactory.Instance);
            Type t = Type.GetType("System.Data.SQLite.EF6.SQLiteProviderServices, System.Data.SQLite.EF6");
            FieldInfo fi = t.GetField("Instance", BindingFlags.NonPublic | BindingFlags.Static);
            SetProviderServices("System.Data.SQLite", (DbProviderServices)fi.GetValue(null));
        }
    }
}