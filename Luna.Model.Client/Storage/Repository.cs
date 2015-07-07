using System;

namespace Luna.Model.Storage
{
    public class Repository : RepositoryBase
    {
        public DateTime LastUpdate { get; set; }

        public Guid Version { get; set; }

        public string Parameters { get; set; }
    }
}