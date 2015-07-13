using System;

namespace Luna.Model
{
    public abstract class ClientEntity : Entity
    {
        protected ClientEntity()
        {
            LastUpdate = DateTime.UtcNow;
            Version = Guid.NewGuid();
        }

        public DateTime LastUpdate { get; set; }

        public Guid Version { get; set; }

        public Guid RepositoryId { get; set; }
    }
}