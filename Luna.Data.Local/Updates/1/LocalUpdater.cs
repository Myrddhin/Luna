using System.Collections.Generic;

namespace Luna.Data.Local.SQLite.Updates.NewMoon
{
    internal class LocalUpdater : SchemaUpdater
    {
        protected override Queue<string> GetUpgradeScripts()
        {
            var buffer = new Queue<string>();
            buffer.Enqueue(@"
CREATE TABLE `Providers`
(
    Id  GUID PRIMARY KEY,
    provider_type TEXT,
    parameters TEXT,
    geolocalizer INTEGER,
    contacts INTEGER)
");

            buffer.Enqueue("INSERT INTO SchemaVersions ( Version) VALUES(1)");

            return buffer;
        }

        protected override Queue<string> GetDowngradeScripts()
        {
            var buffer = new Queue<string>();
            buffer.Enqueue("DROP TABLE Providers");

            return buffer;
        }

        public override decimal Version
        {
            get { return 1; }
        }
    }
}