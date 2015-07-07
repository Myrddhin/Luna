using System;

namespace Luna.Model.Storage
{
    public class RepositorySettings : RepositoryBase
    {
        public bool Default { get; set; }

        public DateTime? LastUsed { get; set; }
    }
}