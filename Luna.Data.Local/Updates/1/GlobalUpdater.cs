﻿using System.Collections.Generic;

namespace Luna.Data.Local.SQLite.Updates.NewMoon
{
    internal class GlobalUpdater : SchemaUpdater
    {
        public override decimal Version
        {
            get { return 1; }
        }

        protected override Queue<string> GetUpgradeScripts()
        {
            var buffer = new Queue<string>();
            buffer.Enqueue(@"
CREATE TABLE `RepositorySettings`
(
    Id  GUID PRIMARY KEY,
    name TEXT,
    is_default INTEGER,
    schema_version NUMERIC,
    online INTEGER,
    last_used DATE NULL
);
");

            buffer.Enqueue("INSERT INTO SchemaVersions ( Version) VALUES(1)");

            return buffer;
        }

        protected override Queue<string> GetDowngradeScripts()
        {
            var buffer = new Queue<string>();
            buffer.Enqueue("DROP TABLE Repositories");

            return buffer;
        }
    }
}